ALTER PROCEDURE [dbo].[spValNegocio]
(@IDSOLICITUD AS INT,@BOTON AS INT,@USUARIO AS INT, @MENSAJE as varchar(500) = '' output,@BANDERA INT=NULL  )
AS

/* Tracker INC-B-1911:JDRA: se agrega el parametro rechazado */
--YAM-P-208 egonzalez 24/08/2015 Se complementó el proceso de recuperación de perfil asignado (analista)
--YAM-P-208 egonzalez 30/09/2015 Se agregó una validación para que busque la última tarea dentro de las tareas activas y no en todas.
--YAM-P-208 egonzalez 04/12/2015 Corrección en la obtención de la última tarea para procesar correctamente las reglas de negocio
--YAM-P-208 JCARRASCO 23/02/2016: Se agrega validación para evitar el cambio de status de la tarea de facturación solo si no se ha registado una factura.
--BBV-P-412: AVH: RQC: 13/09/2016 Configurar la BD correspondiente para conexion a Procotiza
--BBVA-P-423:RQCONYFOR-05 JRHM 29/11/16 Se agrego nueva validacion para que todos los documentos obligatorios sean validados antes de pasar a la siguiente tarea
--BBV-P-423: AMR: 14/12/2016 RQSOL-01 Precalificación Brechas (31, 49, 75)
--BBVA-P-423:RQ03 AVH: 15/12/2016 Se valida la tarea antes de entrar al SP_SCORE
--BBV-P-423 RQCONYFOR-01: AVH: 02/01/2017 Se comenta sp_Facturacion para no consultar a Proleasaenet
--BUG-PD-12: MAGP:20.02.2017: Se cambia formato a mensaje de salida.
--BUG-PD-16: 10/03/2017 MAPH: CAMBIO DE MENSAJE, HOMOLOGACIÓN A 'TAREA EXITOSA'
--BUG-PD-19: JBB:10.03.2017: Modificaciones para tareas automaticas. 
-- BBV-P-423: RQADM-15: JRHM: 17/03/2017 Se agrega condicion en linea 259 para parametro 244 de cuestionario IFAI
-- BBV-P-423: RQADM-37: JRHM: 17/03/2017 Se agrega condicion en linea 300 para parametro 246 de "Solicitud vs ID"
-- BBV-P-423: RQADM-16  JBB:  22/03/2017 Se agrega condicion en linea 269 para parametro 244 de cuestionario Facebook 
-- BBV-P-423: RQADM-17  JBB:  24/03/2017 Se agrega condicion en linea 260 para parametro 281 de cuestionario Linkedin
--BUG-PD-25 JRHM 04/04/17 SE MODIFICO FUNCIONALIDAD PARA PANTALLAS TIPO 68(VALIDACION DE DOCUMENTACION) PARA MANDAR MENSAJE DE ERROR DE "FALTAN TIPIFICAR RECHAZOS" 
-- BBV-P-423: RQADM-09  JBB:  04/04/2017 Se agrega cuestionario cedula profecional. 
--BUG-PD-32: AVH: 18/04/2017 CORRECCIONES DE RHERNANDEZ
--BUG-PD-33 JRHM 24/04/17 SE MODIFICO MENSAJE DE FALTAN POR FALTA EN VALIDACION DE TIPIFICACIONES DE DOCUMENTOS
--BBV-P-423: RQAMD-08: JBEJAR: 24/04/2017 Se agrega condicion linea 261 para parametro 316 de cuestionario INE y se simplica la condicion en un IN.
--BBV-P-423: RQAMD-10: JBEJAR: 24/04/2017 Se agrega condicion linea 261 para parametro 303 de cuestionario cofetel.
--RQADM-38: RHERNANDEZ: 05/05/2017: sE AGREGA VALIDACION DE DOCUMENTOS EN PANTALLA 314
--BBV-P-423: RQADM-25: erodriguez 11/05/2017 Se modifico la linea 738 para asignar al mismo usuario de pool de credito y la 765 para incluirlos en los perfiles aceptados
--BBVA-P-423 RQSOLBCOM-01 GVARGAS 04/04/2017 Permite avanzar Sol de BCOM
--RQXLS3: CGARCIA: 09/06/2017 agrega opcion para valida id solicitud 
--BUG-PD-101: CGARCIA: 16/06/2017 se agrego ejecucion de actualizacion de estatus 
--BUG-PD-98: RHERNANDEZ: 20/06/2017: SE QUITA VALIDA ENTREVISTA DE VALIDACION DE PANTALLAS DOCUMENTALES
--BUG-PD-167: ERODRIGUEZ: 20/07/2017: DE FORMA URGENTE SE AGREGO EL PERFIL 86 A: NO HACE BALANCEO, SIEMPRE ASIGNA AL USUARIO ASIGNADO PREVIAMENTE DE ESE PERFIL
--BUG-PD-174: RHERNANDEZ: 28/07/17: SE CAMBIA FORMA DE VALIDACION DE DOCUMENTOS RECHAZADOS PARA QUE SOLO VALIDE LOS DE LA PANTALLA PROCESADA
--BUG-PD-202: ERODRIGUEZ: 24/08/2017: Se agrego validacion a consulta de balanceo de usuarios para que se haga en base a los usuarios logeados en el dia.
--BUG-PD-206: ERODRIGUEZ: 04/09/2017: Se agrego validacion a consulta de balanceo de usuarios para las tareas automaticas.
--BUG-PD-212: ERODRIGUEZ: 22/09/2017: Se ajusto el tiempo en balanceo de usuario a 30 minutos.
	BEGIN		

		--SECCION VARIABLES	
		DECLARE @NOTAREA AS INT;

		declare @tareaNORechazo int,		/*	Tarea Siguiente by Dave®*/
				@tareaRechazo int;

		DECLARE @RECHAZO INT
		DECLARE @CVETARNREC INT;	
		DECLARE @OPEIDSOLICITUD INT
		DECLARE @TIPO_PANTALLA INT 
		DECLARE @TIPSTATUS INT,@STATUSPROCESO INT
		DECLARE @PROCESOS INT,@PROCESOSIGUI INT,			
			@PANTALLA INT,
			@NOMBREPAN VARCHAR(30),@TIPOCALIFICACION INT,@TIPOCONDICION INT
			--,@MENSAJE VARCHAR(MAX)=''
			--,
			--@RAZO VARCHAR(MAX)=''
		DECLARE @OPENSOLICITUD INT,
				@PERFIL INT
		--DECLARE  @CONDRECHAZO INT	--- D@ve_® -- condicion que nos indica hacia donde se va ir la RN
		declare @statusTareaCancel int			--- Estatus que se le coloca a la solicitud en dado caso de que este cancelada
		declare @statusTareaTerminada int	--- Estatus que se le coloca a la solicitud en dado caso de que este Terminada
		declare @StatusCancelada int			--- Estatus en las RN para que sean canceladas		
		declare @statusCoacreditado int		--- Estatus en las RN para que sean condicionadas a coacreditado	
		declare @tareaCoacreditado int		---Tarea Coacreditado
       DECLARE @PERSONAJURIDICA INT
       DECLARE @IDPERFILTAR INT   ---PERFIL POR TAREA
       DECLARE @IDPERFILSIGUITAR INT ---PERFIL TAREA SIGUIENTE
       DECLARE @NOMBREUSUARI VARCHAR(200)  ---ES PARA SABER A QUIEN SE FUE EL ANALISTA
       DECLARE @IDUSUA INT ----SE EXTRAI DEL LA TAREA
       DECLARE @IDCOTIZACION INT ---SE EXTRAER LA COTIZACION
       DECLARE @IDPRODUCTOS INT ------PRODUCTO DE LA SOLICITUD
	   declare @flujos int
	   declare @ultimaTarea int
		
		select @tareaCoacreditado = pdk_id_tareas
		from PDK_CAT_TAREAS
		where pdk_tar_nombre = 'COACREDITADO';		

		/*		Estatus de rechazo Solicitud	*/
		
		SELECT @statusCoacreditado = parhijo.pdk_id_parametros_sistema
		FROM PDK_PARAMETROS_SISTEMA parpadre
		inner join PDK_PARAMETROS_SISTEMA parhijo
		on parpadre.pdk_id_parametros_sistema = parhijo.pdk_par_sis_id_padre
		where parpadre.pdk_par_sis_parametro =  'STATUS RECHAZO'
		and parhijo.pdk_par_sis_parametro = 'COACREDITADO';
		
		SELECT @StatusCancelada = parhijo.pdk_id_parametros_sistema
		FROM PDK_PARAMETROS_SISTEMA parpadre
		inner join PDK_PARAMETROS_SISTEMA parhijo
		on parpadre.pdk_id_parametros_sistema = parhijo.pdk_par_sis_id_padre
		where parpadre.pdk_par_sis_parametro =  'STATUS RECHAZO'
		and parhijo.pdk_par_sis_parametro = 'CANCELADA';
		
		
		/*	Estatus de tareas	*/

		SELECT @statusTareaCancel =42
		-- parhijo.pdk_id_parametros_sistema
		--FROM PDK_PARAMETROS_SISTEMA parpadre
		--inner join PDK_PARAMETROS_SISTEMA parhijo
		--on parpadre.pdk_id_parametros_sistema = parhijo.pdk_par_sis_id_padre
		--where parpadre.pdk_par_sis_parametro =  'STATUS TAREA'
		--and parhijo.pdk_par_sis_parametro = 'CANCELADA';				 
		
		SELECT @statusTareaTerminada =41
		---- parhijo.pdk_id_parametros_sistema
		----FROM PDK_PARAMETROS_SISTEMA parpadre
		----inner join PDK_PARAMETROS_SISTEMA parhijo
		----on parpadre.pdk_id_parametros_sistema = parhijo.pdk_par_sis_id_padre
		----where parpadre.pdk_par_sis_parametro =  'STATUS TAREA'
		----and parhijo.pdk_par_sis_parametro = 'TERMINADA';				

		 /*		Obtiene persona juridica		*/		 

          SELECT @PERSONAJURIDICA = PDK_ID_PER_JURIDICA,
				@IDPRODUCTOS = PDK_ID_PRODUCTO
          FROM PDK_TAB_SECCION_CERO 
          WHERE PDK_ID_SECCCERO=@IDSOLICITUD;		  

		/*		Obtiene el ID de la maxima tarea de la solicitud		*/

		SELECT @OPEIDSOLICITUD = MAX(PDK_ID_OPE_SOLICITUD) 
		FROM PDK_OPE_SOLICITUD 
		WHERE PDK_ID_SOLICITUD = @IDSOLICITUD;
		
		--select @OPEIDSOLICITUD
		
		/*		Obtiene la informacion de la ultima tarea		*/ /*	Se le agrega la Tarea Siguiente by Dave®*/

		--SELECT	@NOTAREA = os.PDK_ID_TAREAS, 
		--		@tareaNORechazo = PDK_ID_TAREAS_NORECHAZO, 
		--		@tareaRechazo = PDK_ID_TAREAS_RECHAZO
		--FROM PDK_OPE_SOLICITUD os
		--inner join PDK_CAT_TAREAS ct
		--on os.PDK_ID_TAREAS = ct.PDK_ID_TAREAS
		--WHERE PDK_ID_OPE_SOLICITUD = @OPEIDSOLICITUD;		

		--egonzalez
		SELECT
		TOP 1
			@NOTAREA = PDK_CAT_TAREAS.PDK_ID_TAREAS
			,@tareaNORechazo = PDK_CAT_TAREAS.PDK_ID_TAREAS_NORECHAZO
			,@tareaRechazo = PDK_CAT_TAREAS.PDK_ID_TAREAS_RECHAZO
		FROM PDK_OPE_SOLICITUD
		INNER JOIN PDK_CAT_TAREAS
			ON PDK_CAT_TAREAS.PDK_ID_TAREAS = PDK_OPE_SOLICITUD.PDK_ID_TAREAS
		WHERE PDK_OPE_SOLICITUD.PDK_ID_SOLICITUD = @IDSOLICITUD
		ORDER BY PDK_OPE_SOLICITUD.PDK_ID_OPE_SOLICITUD DESC
		print '@NOTAREAACTUAL'
		print @NOTAREA
		--select @NOTAREA
		--return

		select @flujos = c.PDK_ID_FLUJOS, @PROCESOS = b.PDK_ID_PROCESOS
		from PDK_CAT_TAREAS a
		inner join PDK_CAT_PROCESOS b
		on a.PDK_ID_PROCESOS = b.PDK_ID_PROCESOS
		inner join PDK_CAT_FLUJOS c
		on b.PDK_ID_FLUJOS = c.PDK_ID_FLUJOS
		where PDK_ID_TAREAS = @NOTAREA;


		set @ultimaTarea = (
		select top 1 PDK_ID_TAREAS
		from PDK_CAT_TAREAS a
		inner join PDK_CAT_PROCESOS b
		on a.PDK_ID_PROCESOS = b.PDK_ID_PROCESOS
		inner join PDK_CAT_FLUJOS c
		on b.PDK_ID_FLUJOS = c.PDK_ID_FLUJOS
		where c.PDK_ID_FLUJOS = @flujos
		AND a.PDK_TAR_ACTIVO = 2
		order by A.PDK_ID_PROCESOS DESC, PDK_TAR_ORDEN desc) --PARA OBTENER LA ÚLTMA TAREA DEL FLUJO
   					
		--SELECT @PROCESOS=PDK_ID_PROCESOS 
		--FROM PDK_CAT_TAREAS 
		--WHERE PDK_ID_TAREAS =@NOTAREA
			
		--select @PROCESOS

		SELECT	@TIPO_PANTALLA = A.PDK_PANT_DOCUMENTOS, 
				@PANTALLA=A.PDK_ID_PANTALLAS,
				@NOMBREPAN=T.PDK_TAR_NOMBRE  
		FROM PDK_PANTALLAS A
			INNER JOIN PDK_REL_PANTALLA_TAREA B 
			ON B.PDK_ID_TAREAS = B.PDK_ID_TAREAS
			AND B.PDK_ID_PANTALLAS = A.PDK_ID_PANTALLAS
			INNER JOIN PDK_CAT_TAREAS T 
			ON B.PDK_ID_TAREAS =T.PDK_ID_TAREAS  
		WHERE B.PDK_ID_TAREAS = @NOTAREA 
			
		--select @TIPO_PANTALLA;
		--select @PANTALLA;
		
		    -----SELECT PERSONAS JURIDICA  
                  
                  --SELECT @PERSONAJURIDICA=A.PDK_ID_PER_JURIDICA FROM PDK_CAT_FLUJOS A INNER JOIN  PDK_CAT_PROCESOS P ON A.PDK_ID_FLUJOS=P.PDK_ID_FLUJOS
                  --INNER JOIN PDK_CAT_TAREAS T ON T.PDK_ID_PROCESOS=P.PDK_ID_PROCESOS
                  --INNER JOIN PDK_REL_PANTALLA_TAREA TP ON TP.PDK_ID_TAREAS=T.PDK_ID_TAREAS
                  --WHERE TP.PDK_ID_PANTALLAS=@PANTALLA
                  
            ----FIN
            
  		 		  		  
            
		/*	Se separa la validacion de reglas de negocio by D@ve_®*/				

		--SELECT @IDSOLICITUD, @NOTAREA;
		exec sp_ValidaReglasNegocio @IDSOLICITUD, @NOTAREA;		

		declare @respuestaRN bit

		select	@respuestaRN = fnRespuestaRegNeg,  
				@mensaje = fcMensajeRegNeg, 
				@CVETARNREC = fiCVETARNREC
		from ##TempRespValRegNeg
				

		if (@respuestaRN = 1)
		begin 
			select @MENSAJE as 'MENSAJE'
			return;
		end						

		IF EXISTS(SELECT * FROM PDK_TAB_DICTAMEN_FINAL WHERE PDK_ID_SECCCERO = @IDSOLICITUD) AND @NOTAREA = 3
		BEGIN
			IF (SELECT ISNUMERIC(STATUS_DIC) FROM PDK_TAB_DICTAMEN_FINAL WHERE PDK_ID_SECCCERO = @IDSOLICITUD) != 1
			BEGIN
				
				IF(SELECT PDK_TAR_ACTIVO FROM PDK_CAT_TAREAS WHERE PDK_ID_TAREAS = 3 AND PDK_ID_PROCESOS = 1) = 3
				BEGIN
				EXEC SP_SCORE @IDSOLICITUD
			END
		END
		END
		

		/*	Termina Se separa la validacion de reglas de negocio by D@ve_®*/
	
		DECLARE @CONSECT INT, @DICTAMEN VARCHAR(200),@NUMDIC INT,@TSIGUI INT,@CONTADOR INT
	
		--SELECT @CONSECT = (MAX(PDK_ID_OPE_SOLICITUD) + 1)
		--FROM  PDK_OPE_SOLICITUD
	IF @BANDERA IS NOT NULL 
		BEGIN
			SET @CVETARNREC=@BANDERA
		END
				
	
			IF @CVETARNREC IS NULL
			BEGIN				
			
				if @tipo_pantalla = 130
				begin																	

						insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
						exec sp_AsignaEntrevista @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;


				end
                
				else if @TIPO_PANTALLA = 25
				begin
					insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
					exec sp_ValidaCotExterna @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;

				end

                 else if @TIPO_PANTALLA=43		--	Pantalla 43 Dictamen
                 BEGIN
					insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)				
					exec sp_PantallaDictamen @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;																
                 END
                 	ELSE IF @TIPO_PANTALLA = 246 
				 BEGIN
				   
					exec sp_PantallaDocumentos @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;
					insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)					
					values(@idSolicitud, @NOTAREA, @CVETARNREC, @MENSAJE)
					--exec sp_validarEntrevista @idSolicitud,2,@PANTALLA
					declare @MENSAJEV as varchar(max)
					if @MENSAJEV is not null
					begin
					SET @MENSAJE=@MENSAJEV 
					end
					select @MENSAJEV= dbo.fnGetMensajeTarea (@idSolicitud , @PANTALLA)
					IF EXISTS (SELECT * FROM PDK_REL_PAN_DOC_SOL WHERE PDK_ID_SECCCERO =@IDSOLICITUD AND PDK_ST_RECHAZADO = 1)
					BEGIN
						PRINT 'ENTRO DOCU'
						DECLARE @RECHA_1 INT= (SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON 
						A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS INNER JOIN PDK_CAT_DOCUMENTOS C ON A.PDK_ID_DOCUMENTOS = C.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND 
						A.PDK_ST_RECHAZADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_PAR_SIS_PARAMETRO_DIG=0 AND C.PDK_ID_TIPO_DOCUMENTO = 112)
						IF @RECHA_1 >0
						BEGIN
							SELECT @MENSAJE='Falta indicar el motivo de Rechazo' 
						END
						ELSE
						BEGIN
							set @CVETARNREC = @tareaRechazo                        
							UPDATE PDK_REL_PAN_DOC_SOL set PDK_ST_ENTREGADO=0
							WHERE PDK_ID_SECCCERO = @IDSOLICITUD  
							AND PDK_ST_RECHAZADO =1        
							UPDATE PDK_TAB_SECCION_CERO 
							SET PDK_STATUS_DOCUMENTOS=95 
							WHERE PDK_ID_SECCCERO=@IDSOLICITUD                           
							update dbo.PDK_TAB_DATOS_SOLICITANTE 
							set status = 'CONDICIONADO ANALISIS',
							STATUS_DOC='DOCUMENTACION RECHAZADA' 
							where PDK_ID_SECCCERO = @IDSOLICITUD;                          
							SELECT @MENSAJE='SE RECHAZO  DOCUMENTO '                                             
						END 
					END
					ELSE
					BEGIN
						DECLARE @DOCOBLIPANT_1 INT=(SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS inner join PDK_CAT_DOCUMENTOS c on A.PDK_ID_DOCUMENTOS = c.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND B.PDK_REL_ACT_OBLIGATORIO=107 AND B.PDK_ID_PANTALLAS=@PANTALLA and c.PDK_ID_TIPO_DOCUMENTO = 112)
						DECLARE @DOCOBLIPANTSELEC_1 INT=(SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS inner join PDK_CAT_DOCUMENTOS c on A.PDK_ID_DOCUMENTOS = c.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND B.PDK_REL_ACT_OBLIGATORIO=107 AND A.PDK_ST_VALIDADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA and c.PDK_ID_TIPO_DOCUMENTO = 112)
						IF @DOCOBLIPANT_1=@DOCOBLIPANTSELEC_1
						BEGIN				    
							set @CVETARNREC = @tareaNORechazo;             
							update dbo.PDK_TAB_DATOS_SOLICITANTE 
							set status = 'CONDICIONADO ANALISIS',
							STATUS_DOC='DOCUMENTACION COMPLETA' 
							where PDK_ID_SECCCERO = @IDSOLICITUD;
						END
						ELSE
						BEGIN
							DECLARE @RECHAZOS_1 INT= (SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS inner join PDK_CAT_DOCUMENTOS C ON A.PDK_ID_DOCUMENTOS = C.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND A.PDK_ST_RECHAZADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_PAR_SIS_PARAMETRO_DIG=0 AND C.PDK_ID_TIPO_DOCUMENTO = 112)
							IF @RECHAZOS_1 >0
							BEGIN
								SELECT @MENSAJE ='Falta indicar el motivo de Rechazo' 
							END
							ELSE
							BEGIN
								SELECT @MENSAJE='Faltan documentos obligatorios a validar' 											
							END
						END					 
					END 
				 END 
                ELSE IF @TIPO_PANTALLA IN(26,244,280,281,297,303,316 )---ACTUALIZAR EL STATUS DE DOCUMENTO DE 303 -- 246
				--@TIPO_PANTALLA=26 or @TIPO_PANTALLA=244  or  @TIPO_PANTALLA=280  or @TIPO_PANTALLA=281 or @TIPO_PANTALLA =297 or @TIPO_PANTALLA=303  RQAMD-08 se simplica el or en un in 
                 BEGIN                  												

					--select @idSolicitud, @NOTAREA	
					exec sp_PantallaDocumentos @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;
					insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)					
					values(@idSolicitud, @NOTAREA, @CVETARNREC, @MENSAJE)										

                 END
                 
                ELSE IF @TIPO_PANTALLA=116  ---ENTRA CUANDO ES PERSONA MORAL Y CALCULA EL SCOREN
                 BEGIN
					
					insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
					exec sp_PrecalificacionPM @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;				

                 END
				   
                 ELSE IF  @TIPO_PANTALLA=105	--Pantalla facturacion
                 BEGIN                 					                                      																													
				 
			
					set @CVETARNREC = @tareaNORechazo					
					set @MENSAJE = ''
					
					--insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
					--exec sp_Facturacion @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output;

					--SELECT @IDSOLICITUD, @NOTAREA, @CVETARNREC, @MENSAJE
					--TRACKER YAM-P-208 JCARRASCO Se agrega validación si es que falla la facturación.
				   --BUG-PD-16: 10/03/2017 MAPH: CAMBIO DE MENSAJE, HOMOLOGACIÓN A 'TAREA EXITOSA'
					set @MENSAJE = 'TAREA EXITOSA'

				   --BUG-PD-16: 10/03/2017 MAPH: CAMBIO DE MENSAJE, HOMOLOGACIÓN A 'TAREA EXITOSA'
                    if RTRIM(LTRIM(@MENSAJE)) <> 'TAREA EXITOSA'
						BEGIN
							RETURN
						END
                 END              
               
			     ELSE IF @TIPO_PANTALLA=68  or @TIPO_PANTALLA=314 --or @TIPO_PANTALLA=246	SI UN DOCUMENTO ES RECHAZADO TE PASA A LA TAREA DONDE SE CARGA LOS DOCUMENTOS // Tipo pantalla revision de documentos
                 BEGIN				 

                  IF EXISTS (SELECT * 
							FROM PDK_REL_PAN_DOC_SOL A INNER JOIN (SELECT * FROM PDK_REL_PAN_DOC where PDK_ID_PANTALLAS=@PANTALLA) B on A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS 
							WHERE PDK_ID_SECCCERO =@IDSOLICITUD  
							AND PDK_ST_RECHAZADO = 1)
                   BEGIN
                   PRINT 'ENTRO DOCU'
                                 
							/*

                           SELECT @CVETARNREC= A.PDK_ID_TAREAS   
						   FROM PDK_CAT_TAREAS A 
						   INNER JOIN PDK_REL_PANTALLA_TAREA B 
						   ON A.PDK_ID_TAREAS =B.PDK_ID_TAREAS 
                           INNER JOIN PDK_PANTALLAS P 
						   ON  B.PDK_ID_PANTALLAS =P.PDK_ID_PANTALLAS 
                           INNER JOIN PDK_CAT_PROCESOS TP 
						   ON TP.PDK_ID_PROCESOS= A.PDK_ID_PROCESOS
                           INNER JOIN PDK_CAT_FLUJOS FJ 
						   ON FJ.PDK_ID_FLUJOS=TP.PDK_ID_FLUJOS
                           INNER JOIN PDK_CAT_PRODUCTOS PD 
						   ON  PD.PDK_ID_PRODUCTOS=FJ.PDK_ID_PRODUCTOS
                           WHERE P.PDK_PANT_DOCUMENTOS=26 
						   and FJ.PDK_ID_PER_JURIDICA=@PERSONAJURIDICA 
						   AND PD.PDK_ID_PRODUCTOS=@IDPRODUCTOS

						   */						   						   
						 DECLARE @RECHA INT= (SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND A.PDK_ST_RECHAZADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_PAR_SIS_PARAMETRO_DIG=0)
						IF @RECHA >0
						BEGIN
							SELECT @MENSAJE='Falta indicar el motivo de Rechazo' 
						END
						ELSE
						BEGIN
						   set @CVETARNREC = @tareaRechazo
                           
                           UPDATE PDK_REL_PAN_DOC_SOL set PDK_ST_ENTREGADO=0
						   WHERE PDK_ID_SECCCERO = @IDSOLICITUD  
						   AND PDK_ST_RECHAZADO =1
                           --UPDATE PDK_REL_PAN_DOC_SOL SET PDK_ST_ENTREGADO=0 WHERE PDK_ID_SECCCERO =@IDSOLICITUD  AND PDK_ST_RECHAZADO =1
                           UPDATE PDK_TAB_SECCION_CERO 
                           SET PDK_STATUS_DOCUMENTOS=95 
                           WHERE PDK_ID_SECCCERO=@IDSOLICITUD
                           
                           update dbo.PDK_TAB_DATOS_SOLICITANTE 
                           set status = 'CONDICIONADO ANALISIS',
								STATUS_DOC='DOCUMENTACION RECHAZADA' 
							where PDK_ID_SECCCERO = @IDSOLICITUD;
                          
                          SELECT @MENSAJE='SE RECHAZO  DOCUMENTO '                                             
						END                                            
                                                    
                   END 
                   ELSE
                   BEGIN
				   DECLARE @DOCOBLIPANT INT=(SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND B.PDK_REL_ACT_OBLIGATORIO=107 AND B.PDK_ID_PANTALLAS=@PANTALLA)
				   DECLARE @DOCOBLIPANTSELEC INT=(SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND B.PDK_REL_ACT_OBLIGATORIO=107 AND A.PDK_ST_VALIDADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA)
                   IF @DOCOBLIPANT=@DOCOBLIPANTSELEC
				   BEGIN
				     /*
					 SELECT  @CVETARNREC=PDK_ID_TAREAS_NORECHAZO 
                     FROM PDK_CAT_TAREAS   
                     WHERE PDK_ID_TAREAS = @NOTAREA;
                     */

					 set @CVETARNREC = @tareaNORechazo;

                     /*
                     las validaciones fueron cambiadas a la pantalla de documentos. spValidaEstatusDoc
                      UPDATE PDK_TAB_SECCION_CERO 
                      SET PDK_STATUS_DOCUMENTOS=93 
                      WHERE PDK_ID_SECCCERO=@IDSOLICITUD;
                      */
                     
                      update dbo.PDK_TAB_DATOS_SOLICITANTE 
                      set status = 'CONDICIONADO ANALISIS',
						STATUS_DOC='DOCUMENTACION COMPLETA' 
					where PDK_ID_SECCCERO = @IDSOLICITUD;
					END
					ELSE
					BEGIN
						DECLARE @RECHAZOS INT= (SELECT COUNT(*) FROM PDK_REL_PAN_DOC_SOL A INNER JOIN PDK_REL_PAN_DOC B ON A.PDK_ID_DOCUMENTOS=B.PDK_ID_DOCUMENTOS WHERE A.PDK_ID_SECCCERO=@IDSOLICITUD AND A.PDK_ST_RECHAZADO=1 AND B.PDK_ID_PANTALLAS=@PANTALLA AND A.PDK_PAR_SIS_PARAMETRO_DIG=0)
						IF @RECHAZOS >0
						BEGIN
							SELECT @MENSAJE ='Falta indicar el motivo de Rechazo' 
						END
						ELSE
						BEGIN
						SELECT @MENSAJE='Faltan documentos obligatorios a validar' 						
						
					END
					END					 
                   END 				  
                 END
				 
                 
				 ELSE IF  @TIPO_PANTALLA = 79	--	Tipo de Pantalla Cotización
                 BEGIN  						

						--select @idSolicitud, @NOTAREA
						insert tbmensajeTarea (PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
						exec sp_PantallaCotizacion @idSolicitud, @NOTAREA, @CVETARNREC output, @MENSAJE output, @PERSONAJURIDICA;       					                 											
                 END
                 
				 ELSE IF @TIPO_PANTALLA=69 --ENTRA ALA NOTIFICACION		//	Tipo de pantalla autorización
                 BEGIN
                    DECLARE @COMITE INT ,
							@RECHAZOANALISTA INT,
							@RECHAZOCOMITE INT

                    SELECT	@COMITE=PDK_RESULCOMITE ,
							@RECHAZOANALISTA=PDK_RESULRECHAZADO,
							@RECHAZO=PDK_RESULCOMRECH 
					FROM PDK_RESULTADO_NOTIFICA 
					WHERE PDK_ID_SECCCERO=@IDSOLICITUD;

                    IF @COMITE >0
                    BEGIN
                      IF @RECHAZO>0
                       BEGIN
                          SELECT @CVETARNREC=0
                      
                          UPDATE PDK_OPE_SOLICITUD  
                          SET PDK_OPE_STATUS_TAREA=@statusTareaCancel 
						  WHERE PDK_ID_OPE_SOLICITUD = @OPEIDSOLICITUD 
						  AND PDK_ID_SOLICITUD =@IDSOLICITUD;
						
						/*	se actualiza todo el proceso	 by d@ve_®	*/
						
						  UPDATE PDK_OPE_SOLICITUD  
                          SET PDK_OPE_STATUS_PROCESO=@statusTareaCancel
						  WHERE PDK_ID_SOLICITUD =@IDSOLICITUD  
                                                                     
                          SELECT @PERFIL=PDK_ID_USUARIO FROM PDK_REL_USU_PER WHERE PDK_ID_PERFIL IN(SELECT PDK_ID_PERFIL FROM PDK_REL_TAR_PERFIL  WHERE PDK_ID_TAREAS=@NOTAREA)
				          INSERT INTO PDK_NOTIFICACIONES(PDK_ID_SECCCERO,fcUsuarioAlta,fcUsuarioNotificacion,fcNotificacion,fdFecNotificacion)
				          VALUES(@IDSOLICITUD,@PERFIL,2,'SE RECHAZA LA SOLICITUD POR COMITE ', GETDATE())
                       END
                      ELSE
                       BEGIN
                          SELECT  @CVETARNREC=PDK_ID_TAREAS_NORECHAZO FROM PDK_CAT_TAREAS   WHERE PDK_ID_TAREAS = @NOTAREA
                       END
                    END
                    ELSE
                      BEGIN
                        IF @RECHAZOANALISTA>0 
                          BEGIN							

                             SELECT @CVETARNREC=0
                      
                             UPDATE PDK_OPE_SOLICITUD  
                             SET PDK_OPE_STATUS_TAREA=@statusTareaCancel 
						     WHERE PDK_ID_OPE_SOLICITUD = @OPEIDSOLICITUD 
						     AND PDK_ID_SOLICITUD =@IDSOLICITUD;
						
						/*	se actualiza todo el proceso	 by d@ve_®	*/
						
						     UPDATE PDK_OPE_SOLICITUD  
                             SET PDK_OPE_STATUS_PROCESO=@statusTareaCancel
						     WHERE PDK_ID_SOLICITUD =@IDSOLICITUD  
                                                                     
                             SELECT @PERFIL=PDK_ID_USUARIO FROM PDK_REL_USU_PER WHERE PDK_ID_PERFIL IN(SELECT PDK_ID_PERFIL FROM PDK_REL_TAR_PERFIL  WHERE PDK_ID_TAREAS=@NOTAREA)
				             INSERT INTO PDK_NOTIFICACIONES(PDK_ID_SECCCERO,fcUsuarioAlta,fcUsuarioNotificacion,fcNotificacion,fdFecNotificacion)
				             VALUES(@IDSOLICITUD,@PERFIL,2,'SE RECHAZA LA SOLICITUD POR COMITE ', GETDATE())
                          END
                        ELSE
                          BEGIN
                            SELECT  @CVETARNREC=PDK_ID_TAREAS_NORECHAZO 
							FROM PDK_CAT_TAREAS   
                            WHERE PDK_ID_TAREAS = @NOTAREA
                          END  
                      END
                 END
                 
                 ELSE
                 BEGIN    
				 
					--select 'Entra otra vez', @NOTAREA
                  
				  SELECT @CVETARNREC = PDK_ID_TAREAS_NORECHAZO
				  FROM PDK_CAT_TAREAS 
				  WHERE PDK_ID_TAREAS = @NOTAREA;

				  IF @CVETARNREC = 0
				  BEGIN
					SET @MENSAJE = 'ERROR: NO EXISTE TAREA SIGUIENTE'
				  END
				  
				  SELECT @PROCESOSIGUI = PDK_ID_PROCESOS 
				  FROM PDK_CAT_TAREAS  
				  WHERE PDK_ID_TAREAS =@CVETARNREC	   
				  				  
				 END				 				 				 				 
				 				 
			END							
			
		/*	Seccion que valida si existe un perfil que pueda realizar la tarea siguiente	*/

		if @CVETARNREC IS NOT NULL

		begin

		---- @CVETARNREC ----- Tarea siguiente.

		SELECT @IDPERFILTAR=PDK_ID_PERFIL 
		FROM PDK_REL_TAR_PERFIL 
		WHERE PDK_ID_TAREAS = @CVETARNREC;
		PRINT 'CLAVETAREASIG-PERFILTAREASIG'
		PRINT @CVETARNREC
		PRINT @IDPERFILTAR
		print @ultimaTarea
		print @NOTAREA
		IF @ultimaTarea<>@NOTAREA AND NOT EXISTS   (SELECT * 
						FROM PDK_REL_USU_DIST RD 
						INNER JOIN PDK_CAT_DISTRIBUIDOR DIS 
						ON DIS.PDK_ID_DISTRIBUIDOR=RD.PDK_ID_DISTRIBUIDOR
						inner join PDK_USUARIO pu
						on rd.PDK_ID_USUARIO = pu.PDK_ID_USUARIO
						WHERE RD.PDK_ID_USUARIO IN(SELECT PDK_ID_USUARIO 
												FROM PDK_REL_USU_PER 
												WHERE PDK_ID_PERFIL = @IDPERFILTAR
												UNION
												SELECT UP.PDK_ID_USUARIO
												FROM PDK_REL_NIVEL_PERFIL NP 
												INNER JOIN PDK_REL_USU_PER UP 
												ON UP.PDK_ID_PERFIL=NP.PDK_NIVELPER_NUMNIVEL 
												WHERE NP.PDK_ID_PERFIL=@IDPERFILTAR 
												AND NP.PDK_NIVELPER_STATUS=1 
												AND NP.PDK_NIVELPER_NUMNIVEL<>1 )
						AND DIS.PDK_DIST_CLAVE=(SELECT PDK_DIST_CLAVE 
												FROM PDK_TAB_SECCION_CERO 
												WHERE PDK_ID_SECCCERO=@IDSOLICITUD)
						and dis.PDK_DIST_ACTIVO = 2	--Distribuidor Activo
						and pu.PDK_USU_ACTIVO = 2)	--Usuario Activo
		BEGIN
		  SET @MENSAJE = 'ERROR: NO HAY USUARIO ASIGNADO PARA  DEALER COMUNICARSE CON EL ADMINISTRADOR DEL SISTEMA'
		  insert tbmensajeTarea(PDK_ID_SECCCERO, PDK_ID_TAREAS, PDK_ID_NVA_TAREA, PDK_MENSAJE_TAREA)
		  select @idSolicitud, @NOTAREA, @CVETARNREC, @MENSAJE;
		  SELECT @MENSAJE AS 'MENSAJE' 
		  RETURN;
		END

		end
		
		SELECT @IDPERFILTAR=PDK_ID_PERFIL 
		FROM PDK_REL_TAR_PERFIL 
		WHERE PDK_ID_TAREAS = @NOTAREA;
		PRINT 'CLAVETAREAACTUAL-PERFILTAREACTUAL'
		PRINT @CVETARNREC
		PRINT @IDPERFILTAR
		/*	Seccion que valida si existe un perfil que pueda realizar la tarea siguiente	*/

						
		 ------/*******Actualizar el status  ************


		 /*se realiza cambio @ultimaTarea*/

		 --DECLARE @MAXTAREA INT		 
		  
		 -- SELECT @OPENSOLICITUD=MAX(PDK_ID_OPE_SOLICITUD) 
		 -- FROM PDK_OPE_SOLICITUD  
		 -- WHERE  PDK_ID_SOLICITUD =@IDSOLICITUD 
		 -- AND PDK_ID_TAREAS=@NOTAREA
		  
		 --  SELECT @MAXTAREA=MAX(T.PDK_TAR_ORDEN)	--- cambio para actualizar status
		 --  FROM PDK_CAT_FLUJOS A 
		 --  INNER JOIN  PDK_CAT_PROCESOS P 
		 --  ON A.PDK_ID_FLUJOS=P.PDK_ID_FLUJOS
   --      INNER JOIN PDK_CAT_TAREAS T 
		 --ON T.PDK_ID_PROCESOS=P.PDK_ID_PROCESOS
   --      INNER JOIN PDK_REL_PANTALLA_TAREA TP 
		 --ON TP.PDK_ID_TAREAS=T.PDK_ID_TAREAS
   --         INNER JOIN PDK_CAT_PRODUCTOS PD 
			--ON  PD.PDK_ID_PRODUCTOS=A.PDK_ID_PRODUCTOS 
   --      WHERE A.PDK_ID_PER_JURIDICA=@PERSONAJURIDICA 
		 --AND PD.PDK_ID_PRODUCTOS=@IDPRODUCTOS
 					
		
		  --SELECT @MAXTAREA=MAX(PDK_ID_TAREAS) FROM PDK_CAT_TAREAS		  

		  IF @CVETARNREC<>0
		  BEGIN
     	      UPDATE PDK_OPE_SOLICITUD
		      SET PDK_OPE_STATUS_TAREA=@statusTareaTerminada,				 
				 PDK_OPE_FECHA_FINAL=GETDATE()						
	          WHERE  PDK_ID_SOLICITUD =@IDSOLICITUD 
		      and  PDK_ID_OPE_SOLICITUD = @OPEIDSOLICITUD;
		     
		    /*		Se actualiza todo el proceso como terminado by d@ve_®	*/  
		     
		    UPDATE PDK_OPE_SOLICITUD
		      SET PDK_OPE_STATUS_PROCESO=@statusTareaTerminada
	          WHERE  PDK_ID_SOLICITUD =@IDSOLICITUD 		      
		        
		  END
		  
		 
		  IF @ultimaTarea=@NOTAREA
		     BEGIN

		       UPDATE PDK_OPE_SOLICITUD
		       SET PDK_OPE_STATUS_TAREA=118,			
					PDK_OPE_FECHA_FINAL=GETDATE()						
	           WHERE  PDK_ID_SOLICITUD =@IDSOLICITUD
		       and  PDK_ID_OPE_SOLICITUD = @OPEIDSOLICITUD;
		       
		       /*		Se actualiza todo el proceso	*/
		       
		       UPDATE PDK_OPE_SOLICITUD
		       SET PDK_OPE_STATUS_PROCESO=118
	           WHERE  PDK_ID_SOLICITUD =@IDSOLICITUD 

			   --BUG-PD-16: 10/03/2017 MAPH: CAMBIO DE MENSAJE, HOMOLOGACIÓN A 'TAREA EXITOSA'
			   SET @MENSAJE = 'TAREA EXITOSA'
			   --SET @MENSAJE = 'PROCESO FINALIZADO CON EXITO.'
		       		       
		     END	
		
	
	-------/*********************verificar el coacreditrado  *****************
	        DECLARE @NUMERO_PANTALLA INT
				
		           SELECT @NUMERO_PANTALLA=	P.PDK_PANT_DOCUMENTOS  
		           FROM PDK_CAT_TAREAS A 
		           INNER JOIN PDK_REL_PANTALLA_TAREA R 
		           ON A.PDK_ID_TAREAS=R.PDK_ID_TAREAS
                  INNER JOIN PDK_PANTALLAS P 
                  ON R.PDK_ID_PANTALLAS=P.PDK_ID_PANTALLAS 
                  WHERE  A.PDK_ID_TAREAS=@CVETARNREC
				    
		 
		 IF @NUMERO_PANTALLA=78
		    BEGIN
		       IF @CONTADOR=2
		        BEGIN
		            --select 'SE SOLICITA COACREDITADO'
                  --if  NOT exists   (SELECT * FROM PDK_TAB_COACREDITADO_DOS WHERE PDK_ID_SECCCERO =@IDSOLICITUD)
                  -- BEGIN
                    PRINT ('ENTRA PARA INSERTAR EL COACREDITADO')
		            EXEC sp_insertarCoacreditado2 @IDSOLICITUD
                   ----END
		        END 
             
		     
		    END
		    
		    --select @TIPO_PANTALLA;
			
		DECLARE @BCOM INT = ( SELECT PDK_ID_SECCCERO FROM PDK_DATOS_PRESTAMO WHERE PDK_ID_SECCCERO = @IDSOLICITUD)
			
		IF ((@TIPO_PANTALLA = 34 OR @TIPO_PANTALLA = 25) AND (@BCOM IS NULL))
			--IF @TIPO_PANTALLA = 34 OR @TIPO_PANTALLA = 25
			BEGIN
				--select 'ENTRA SCORE'
				--select @IDSOLICITUD;

					/*	JDRA Actualiza el plazo de acuerdo a el valor de la cotización.	*/

						declare @plazo int, @pago_Mensual decimal(18,2) 


						--select @plazo = case when b.texto = 'MENSUAL'
						--					then valor_plazo 
						--				else valor_plazo / 2
						--			end,
						--		@pago_Mensual = case when b.texto = 'MENSUAL'
						--				then pago_periodo 
						--			else pago_periodo * 2
						--		end
						--from PDK_TAB_DATOS_SOLICITANTE ds		
						--inner join (select ID_COTIZA, pago_periodo + pago_periodo_seguro pago_periodo, valor_plazo, id_periodicidad
						--			from bmnpad01..paso_cotiza pc
						--			inner join bmnpad01..REL_PASCOT_COTIZA rpc
						--			on pc.id_cot_paso = rpc.ID_COT_PASO) co
						--on ds.NUM_COTIZACION = co.ID_COTIZA
						----inner join PDK_TAB_DATOS_CREDITO dc
						----on ds.PDK_ID_SECCCERO = dc.PDK_ID_SECCCERO
						--inner join bmnpad01..parametros_sistema b
						--on co.id_periodicidad = b.id_parametro
						--where ds.PDK_ID_SECCCERO = @IDSOLICITUD


						--BBV-P-412:AVH
						SELECT @plazo        = case when C.TEXTO = 'MENSUAL'then B.VALOR_PLAZO else B.VALOR_PLAZO / 2 end,
							   @pago_Mensual = case when C.TEXTO = 'MENSUAL'then B.PAGO_PERIODO else B.PAGO_PERIODO * 2 end 
						FROM PDK_TAB_DATOS_SOLICITANTE A
						INNER JOIN bmnpad01..COTIZACIONES B ON A.NUM_COTIZACION = B.ID_COTIZACION
						INNER JOIN bmnpad01..PARAMETROS_SISTEMA C ON B.ID_PERIODICIDAD = C.ID_PARAMETRO
						WHERE PDK_ID_SECCCERO=@IDSOLICITUD
						

						if @plazo is null or @pago_Mensual is null
						begin
							if @plazo is null
								begin
									set @MENSAJE = 'Error: El plazo no puede estar vacio, realizar nueva cotizacion'
								end
							else
								begin
									set @MENSAJE = 'Error: El pago mensual no puede estar vacio, realizar nueva cotizacion'
								end

							SET @CVETARNREC = 1
							--select @MENSAJE as MENSAJE;
							--return;
						end
						else
						begin

							update PDK_TAB_DATOS_CREDITO
							set plazo = @plazo,
								MENSUALIDAD = @pago_Mensual											
							where PDK_ID_SECCCERO = @IDSOLICITUD;

							update a
							set ID_COTIZACION = NUM_COTIZACION
							from PDK_TAB_SECCION_CERO a
							inner join PDK_TAB_DATOS_SOLICITANTE b
							on a.PDK_ID_SECCCERO = b.PDK_ID_SECCCERO
							where a.PDK_ID_SECCCERO = @IDSOLICITUD

							EXEC SP_SCORE @IDSOLICITUD;

						end						
												
					/*	Termina Actualiza el plazo de acuerdo a el valor de la cotización.	*/							
						
			END
			
           -----Obtenere el perfil de la siguiente tarea 
       
		  if @CVETARNREC>0
		  BEGIN
			----VALIDA SI ESTA EN POOL DE CREDITO
			IF EXISTS(SELECT * FROM PDK_REL_COT_POOL WHERE PDK_ID_SOLICITUD = @IDSOLICITUD AND ESTATUS_ATENCION = 1 AND ESTATUS = 1)
			BEGIN
			--*********************************************************************************************************--
			--DECLARE @USU_ASIGNADO	INT
			--	SELECT @USU_ASIGNADO = PDK_OPE_USU_ASIGNADO FROM PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = @IDSOLICITUD
			--	ORDER BY PDK_ID_OPE_SOLICITUD ASC
			----SELECT @USU_ASIGNADO
			--IF @USU_ASIGNADO <> @USUARIO
			--BEGIN
				
			--END
			--ELSE 
			--BEGIN
			--	SET @IDPERFILSIGUITAR = 86		 
			--END
			--*********************************************************************************************************--
				SET @IDPERFILSIGUITAR = 86		 
			END
			ELSE
			  BEGIN
				 SELECT @IDPERFILSIGUITAR=PDK_ID_PERFIL 
				 FROM PDK_REL_TAR_PERFIL 
				 WHERE PDK_ID_TAREAS=@CVETARNREC
				END
			END		  
		  
			
		
           --------fin
			   
				
		/*Balanceo de tareas*/				

		declare @usrAsignado int = 0
		declare @usrAsignadoTMP int = 0

		--select @IDPERFILTAR, @IDPERFILSIGUITAR
		print 'A'
		print @IDPERFILTAR
		print @IDPERFILSIGUITAR
		
		IF isnull(@IDPERFILTAR, 0) != isnull(@IDPERFILSIGUITAR, 0)
		BEGIN				
			--SE AGREGO 86 PARA POOL CREDITO
			--SELECT * FROM PDK_PERFIL
		if @IDPERFILSIGUITAR in (SELECT PDK_ID_PERFIL FROM PDK_PERFIL)
		begin
			if OBJECT_ID('tempdb..#temp_Usuario_Solicitud') is not null
			drop table #temp_Usuario_Solicitud;
			if OBJECT_ID('tempdb..#temp_Usuario_Solicitud_2') is not null
			drop table #temp_Usuario_Solicitud_2;
			if OBJECT_ID('tempdb..#temp_Usuario_Solicitud_3') is not null
			drop table #temp_Usuario_Solicitud_3;
			--SELECT * FROM PDK_PERFIL
			IF @IDPERFILSIGUITAR IN (7)--LE ASIGNA EL USUARIO QUE HIZO LA ULTIMA TAREA
			BEGIN
				SELECT TOP 1 @usrAsignadoTMP = PDK_OPE_USU_ASIGNADO FROM PDK_OPE_SOLICITUD
				WHERE PDK_OPE_USU_ASIGNADO <> (
					SELECT TOP 1 PDK_OPE_USU_ASIGNADO FROM PDK_OPE_SOLICITUD
					WHERE PDK_ID_SOLICITUD = @IDSOLICITUD
					ORDER BY PDK_ID_OPE_SOLICITUD ASC
				)
					AND PDK_ID_SOLICITUD = @IDSOLICITUD
				ORDER BY PDK_ID_OPE_SOLICITUD ASC
				print 'B'
				PRINT @usrAsignadoTMP
			END

			IF @IDPERFILSIGUITAR IN (71,72,86,84)--78--NO HACE BALANCEO, SIEMPRE ASIGNA AL USUARIO ASIGNADO PREVIAMENTE DE ESE PERFIL
			BEGIN
				SELECT TOP 1 @usrAsignadoTMP = PDK_OPE_USU_ASIGNADO 
				FROM PDK_OPE_SOLICITUD
				WHERE PDK_OPE_USU_ASIGNADO IN (SELECT PDK_ID_USUARIO FROM PDK_REL_USU_PER WHERE PDK_ID_PERFIL = @IDPERFILSIGUITAR)
				AND PDK_ID_SOLICITUD = @IDSOLICITUD
				ORDER BY PDK_ID_OPE_SOLICITUD DESC	
				print 'C'
				PRINT @usrAsignadoTMP
				PRINT @IDPERFILSIGUITAR
			END

			if @usrAsignadoTMP <> 0
			BEGIN
				SELECT @usrAsignado = @usrAsignadoTMP
				print 'D'
				print @usrAsignado
			END
			ELSE
			BEGIN--HACE LA ASIGNACION EN BASE AL BALANCEO

			if((select PDK_PANT_MOSTRAR from PDK_PANTALLAS pp inner join PDK_REL_PANTALLA_TAREA prpt on prpt.PDK_ID_PANTALLAS = pp.PDK_ID_PANTALLAS	where prpt.PDK_ID_TAREAS = @CVETARNREC)<>2)
			begin
				select a.PDK_ID_USUARIO, count(b.PDK_ID_SOLICITUD) noSolicitudes
				into #temp_Usuario_Solicitud
				from PDK_REL_USU_PER a
				inner join PDK_USUARIO c
				on a.PDK_ID_USUARIO = c.PDK_ID_USUARIO
				left outer join PDK_OPE_SOLICITUD b
				on a.PDK_ID_USUARIO = b.PDK_OPE_USU_ASIGNADO
				and PDK_OPE_STATUS_TAREA = 40
				where PDK_ID_PERFIL = @IDPERFILSIGUITAR 				
				and PDK_USU_ACTIVO = 2
				group by a.PDK_ID_USUARIO
				--SELECT * FROM  #temp_Usuario_Solicitud
				
						select @usrAsignado = a.PDK_ID_USUARIO
						from #temp_Usuario_Solicitud a
						inner join (
							select min(noSolicitudes)noSolicitudes
							from #temp_Usuario_Solicitud) b
						on a.noSolicitudes = b.noSolicitudes;		
					
			end
			else
						
				begin
					select a.PDK_ID_USUARIO, count(b.PDK_ID_SOLICITUD) noSolicitudes
					into #temp_Usuario_Solicitud_2
					from PDK_REL_USU_PER a
					inner join PDK_USUARIO c
					on a.PDK_ID_USUARIO = c.PDK_ID_USUARIO
					left outer join PDK_OPE_SOLICITUD b
					on a.PDK_ID_USUARIO = b.PDK_OPE_USU_ASIGNADO
					and PDK_OPE_STATUS_TAREA = 40
					where PDK_ID_PERFIL = @IDPERFILSIGUITAR 
					--and CONVERT(DATE,C.HORA_INGRESO) = CONVERT(DATE,GETDATE()) --- BUG-PD-202 Se obtienen solo los usuarios que se logearon en el dia
					AND DATEADD(MINUTE,30,C.HORA_INGRESO ) >= GETDATE() --BUG-PD-212
					and PDK_USU_ACTIVO = 2
					group by a.PDK_ID_USUARIO
					--SELECT * FROM  #temp_Usuario_Solicitud
					if (select count(PDK_ID_USUARIO) from #temp_Usuario_Solicitud_2) > 0
						BEGIN	
							select @usrAsignado = a.PDK_ID_USUARIO
							from #temp_Usuario_Solicitud_2 a
							inner join (
								select min(noSolicitudes)noSolicitudes
								from #temp_Usuario_Solicitud_2) b
							on a.noSolicitudes = b.noSolicitudes;
						END
					ELSE
						BEGIN --- BUG-PD-206 Se hace balanceo normal.
																
								select a.PDK_ID_USUARIO, count(b.PDK_ID_SOLICITUD) noSolicitudes
								into #temp_Usuario_Solicitud_3
								from PDK_REL_USU_PER a
								inner join PDK_USUARIO c
								on a.PDK_ID_USUARIO = c.PDK_ID_USUARIO
								left outer join PDK_OPE_SOLICITUD b
								on a.PDK_ID_USUARIO = b.PDK_OPE_USU_ASIGNADO
								and PDK_OPE_STATUS_TAREA = 40
								where PDK_ID_PERFIL = @IDPERFILSIGUITAR 				
								and PDK_USU_ACTIVO = 2
								group by a.PDK_ID_USUARIO
								--SELECT * FROM  #temp_Usuario_Solicitud
				
										select @usrAsignado = a.PDK_ID_USUARIO
										from #temp_Usuario_Solicitud_3 a
										inner join (
											select min(noSolicitudes)noSolicitudes
											from #temp_Usuario_Solicitud_3) b
										on a.noSolicitudes = b.noSolicitudes;		
						END

				end
		

				
			
				
			PRINT 'E'
			PRINT @IDPERFILSIGUITAR
			PRINT @usrAsignado
			END
		end
		else 
		begin
			set @usrAsignado = (
			select top 1 PDK_CLAVE_USUARIO usuarioAsignado
			from PDK_OPE_SOLICITUD
			where PDK_ID_SOLICITUD = @IDSOLICITUD
			order by PDK_ID_OPE_SOLICITUD)
			print 'F'
			PRINT @usrAsignado
		end						


		--select @usrAsignado --- usuario asignado
		 
		  ---mandar al F&I O VENDEDOR QUIEN TIENE ESA SOLICITUD
		  IF ISNULL(@usrAsignado,0)>0
		  BEGIN
		    SELECT @NOMBREUSUARI = PDK_USU_NOMBRE+' '+PDK_USU_APE_PAT+' '+PDK_USU_APE_MAT 
			FROM PDK_USUARIO 
			WHERE PDK_ID_USUARIO=@usrAsignado;

		    SELECT @IDUSUA = PDK_OPE_USU_ASIGNADO 
			FROM PDK_OPE_SOLICITUD 
			WHERE PDK_ID_TAREAS=@NOTAREA 
		    ----SE BORRA
		   DELETE FROM PDK_NOTIFICACIONES 
		   WHERE PDK_ID_SECCCERO=@IDSOLICITUD 
		   AND fcUsuarioAlta=@usrAsignado 
		   AND fcNotificacion LIKE '%SE ASIGNO%'
		    ---SE INSERTA
		    
		    INSERT INTO PDK_NOTIFICACIONES(PDK_ID_SECCCERO,fcUsuarioAlta,fcUsuarioNotificacion,fcNotificacion,fdFecNotificacion)
		    VALUES(@IDSOLICITUD,@usrAsignado,@IDUSUA,'SE ASIGNO '+@NOMBREUSUARI, GETDATE())

		  END
		  
		  
		 
		END						
				

		IF(select COUNT(*) from PDK_TAB_SECCION_CERO where PDK_ID_SECCCERO=@IDSOLICITUD AND PDK_TAREA_ACTUAL=86)>0
				BEGIN
					UPDATE PDK_TAB_SECCION_CERO SET PDK_STATUS_CREDITO=295
					WHERE PDK_ID_SECCCERO=@IDSOLICITUD
				END

				
		if @CVETARNREC<>0

		begin

		  INSERT INTO PDK_OPE_SOLICITUD
           (PDK_ID_SOLICITUD
           ,PDK_ID_CAT_RESULTADO
           ,PDK_ID_CAT_RECHAZOS
           ,PDK_ID_TAREAS
           ,PDK_OPE_STATUS
           ,PDK_OPE_MODIF
           ,PDK_CLAVE_USUARIO
           ,PDK_OPE_USU_ASIGNADO
           ,PDK_OPE_STATUS_TAREA
           ,PDK_OPE_STATUS_PROCESO
           ,PDK_OPE_FECHA_INICIO
           ,PDK_OPE_FECHA_FINAL)
		SELECT top 1 
			  PDK_ID_SOLICITUD
			  ,PDK_ID_CAT_RESULTADO
			  ,PDK_ID_CAT_RECHAZOS
			  ,CASE WHEN @CVETARNREC = 0 THEN PDK_ID_TAREAS ELSE @CVETARNREC END
			  ,PDK_OPE_STATUS
			  ,GETDATE()
			  ,PDK_CLAVE_USUARIO
			  ,CASE WHEN ISNULL(@usrAsignado,0) = 0 
						THEN PDK_OPE_USU_ASIGNADO 
					ELSE @usrAsignado END
			  ,40
			  ,40
			  ,GETDATE()
			  ,NULL
		  FROM PDK_OPE_SOLICITUD
		  WHERE PDK_ID_TAREAS = @NOTAREA
			AND PDK_ID_SOLICITUD = @IDSOLICITUD
				
				SET @CONSECT=@@IDENTITY
				
		    UPDATE PDK_TAB_SECCION_CERO SET PDK_TAREA_ACTUAL=@CVETARNREC 
		    WHERE PDK_ID_SECCCERO=@IDSOLICITUD
						
			IF(select COUNT(*) from PDK_TAB_SECCION_CERO where PDK_ID_SECCCERO=@IDSOLICITUD AND PDK_STATUS_CREDITO=104)>0
				BEGIN
					UPDATE PDK_TAB_SECCION_CERO SET PDK_STATUS_CREDITO=231
					WHERE PDK_ID_SECCCERO=@IDSOLICITUD
				END
						
		end													
				
		---************INSERTAMOS LA BITACORA ***************************		---------
		DECLARE @IDPERFIL AS INT
		
		SELECT @IDPERFIL = PDK_ID_PERFIL 
		FROM PDK_REL_USU_PER 
		WHERE PDK_ID_USUARIO =@USUARIO
		
		  INSERT INTO PDK_TAREA_BITACORA(
		               PDK_ID_USUARIO,
		               PDK_ID_PERFIL,
		               PDK_ID_PANTALLA,
		               PDK_BITACORA_MODIF,
		               PDK_CLAVE_USUARIO,
		               PDK_ID_SOLICITUD,
		               PDK_BITACORA_STATUS )
		       VALUES (@USUARIO,
		               @IDPERFIL,
		               @PANTALLA,
		               GETDATE(),
		               @USUARIO,
		               @IDSOLICITUD,
		               @BOTON)
				
				
		 -- REVISAMOS SI LA SIGUIENTE PANTALLA - TAREA SE MUESTRA O NO
		 DECLARE @PANTALLA_AUTOMATICA INT
		 DECLARE @TAREA_PROC INT,@DOCUMEN INT,@IDPANTALLA INT
		 
		 SET @PANTALLA_AUTOMATICA = 0 
		 SET @TAREA_PROC = 0 
		
		 SELECT @TAREA_PROC = A.PDK_ID_TAREAS 
		 FROM PDK_OPE_SOLICITUD A 
		 WHERE PDK_ID_OPE_SOLICITUD = @CONSECT
		 
		 SELECT @PANTALLA_AUTOMATICA = B.PDK_PANT_MOSTRAR 
		 FROM PDK_REL_PANTALLA_TAREA A
		INNER JOIN PDK_PANTALLAS B 
		ON B.PDK_ID_PANTALLAS = A.PDK_ID_PANTALLAS
		AND A.PDK_ID_TAREAS = @TAREA_PROC;
									
		SELECT @DOCUMEN = A.PDK_PANT_DOCUMENTOS  
		FROM PDK_PANTALLAS A
		INNER JOIN PDK_REL_PANTALLA_TAREA B 
		ON B.PDK_ID_PANTALLAS =A.PDK_ID_PANTALLAS 		
		WHERE B.PDK_ID_TAREAS =@TAREA_PROC	
					
         IF @DOCUMEN<>27
         BEGIN
		    IF @PANTALLA_AUTOMATICA = 3 
			  BEGIN			  
					--select 'entra ciclo'
			    	EXEC [spValNegocio] @IDSOLICITUD ,@BOTON,@USUARIO
			   END
		  END			

		
		EXEC SP_ASIGNA_ESTATUS @IDSOLICITUD  
		SELECT @MENSAJE	AS MENSAJE;		

	END
