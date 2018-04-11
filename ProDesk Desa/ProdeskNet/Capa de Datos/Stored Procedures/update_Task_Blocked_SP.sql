ALTER PROCEDURE update_Task_Blocked_SP
    @PDK_ID_SOLICITUD INT
AS
BEGIN
	--BUG-PD-200 GVARGAS 24/08/2017 Regresa tareas atrapadas
	--BBVA-P-423 RQ-MN2-1 GVARGAS 07/09/2017 CI Precalificaciòn
	
	--EXEC update_Task_Blocked_SP 1761

	DECLARE @TASK INT = ( SELECT TOP 1 B.PDK_PANT_MOSTRAR FROM PDK_OPE_SOLICITUD A
		 				   INNER JOIN PDK_PANTALLAS B
							  ON B.PDK_ID_PANTALLAS = A.PDK_ID_TAREAS
						   WHERE A.PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD
						   ORDER BY A.PDK_ID_OPE_SOLICITUD DESC )

	DECLARE @ID_TASK INT = ( SELECT TOP 1 PDK_ID_TAREAS FROM PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD ORDER BY PDK_ID_OPE_SOLICITUD DESC )

	IF @TASK <> 2
	BEGIN
		DECLARE @LAST_ID_OPE_SOLICITUD INT = ( SELECT TOP 1 A.PDK_ID_OPE_SOLICITUD FROM PDK_OPE_SOLICITUD A
 											    INNER JOIN PDK_PANTALLAS B
												   ON B.PDK_ID_PANTALLAS = A.PDK_ID_TAREAS
												WHERE A.PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD
												  AND B.PDK_PANT_MOSTRAR = 2
												ORDER BY A.PDK_ID_OPE_SOLICITUD DESC )

		DECLARE @LAST_ID_TASK INT = ( SELECT TOP 1 A.PDK_ID_TAREAS FROM PDK_OPE_SOLICITUD A
 							 			     INNER JOIN PDK_PANTALLAS B
											    ON B.PDK_ID_PANTALLAS = A.PDK_ID_TAREAS
											 WHERE A.PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD
											   AND B.PDK_PANT_MOSTRAR = 2
											 ORDER BY A.PDK_ID_OPE_SOLICITUD DESC )
	
		DELETE PDK_OPE_SOLICITUD WHERE PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD AND PDK_ID_OPE_SOLICITUD > @LAST_ID_OPE_SOLICITUD;

		UPDATE PDK_OPE_SOLICITUD SET PDK_OPE_STATUS_PROCESO = 40, PDK_OPE_STATUS_TAREA = 40 WHERE PDK_ID_SOLICITUD = @PDK_ID_SOLICITUD AND PDK_ID_OPE_SOLICITUD = @LAST_ID_OPE_SOLICITUD;

		UPDATE PDK_TAB_SECCION_CERO SET PDK_CONTADOR = 0, PDK_TAREA_ACTUAL = @LAST_ID_TASK WHERE PDK_ID_SECCCERO = @PDK_ID_SOLICITUD

		INSERT INTO PDK_TAB_RETURNED_TASKS VALUES (@PDK_ID_SOLICITUD, @ID_TASK, @LAST_ID_TASK, GETDATE())
	END
END