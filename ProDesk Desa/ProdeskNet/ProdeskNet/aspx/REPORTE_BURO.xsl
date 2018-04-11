<?xml version="1.0" encoding="iso-8859-1"?>
<!DOCTYPE xsl:stylesheet  [
	<!ENTITY nbsp   "&#160;">
	<!ENTITY copy   "&#169;">
	<!ENTITY reg    "&#174;">
	<!ENTITY trade  "&#8482;">
	<!ENTITY mdash  "&#8212;">
	<!ENTITY ldquo  "&#8220;">
	<!ENTITY rdquo  "&#8221;">
	<!ENTITY pound  "&#163;">
	<!ENTITY yen    "&#165;">
	<!ENTITY euro   "&#8364;">
]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" encoding="ISO-8859-1" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
	<xsl:template match="/">
		<html>
      <!--  'Tracker INC-B-2114:JDRA:Cambio en el area de SC    -->
			<head>
				<title>
					Reporte de Credito -
					<xsl:value-of select="Reporte/PN/C02"/>&nbsp;
					<xsl:value-of select="Reporte/PN/C03"/>&nbsp;
					<xsl:value-of select="Reporte/PN/Paterno"/>&nbsp;
					<xsl:value-of select="Reporte/PN/C00"/>&nbsp;
					<xsl:value-of select="Reporte/PN/C01"/>&nbsp;
				</title>
				<link href="ReporteBC.css" rel="stylesheet" type="text/css" />
			</head>
			<body class="bodyBC">
				<p align="center">
					<strong>REPORTE DE CREDITO </strong>
					<br />
					<span class="thSBC">DOCUMENTO SIN VALOR PROBATORIO EN JUICIOS</span>
				</p>
				<table border="1" class="tableBC">
					<tr>
						<th colspan="6" class="thTBC">DATOS GENERALES</th>
					</tr>
					<tr>
						<th scope="col">NOMBRE(S)</th>
						<th scope="col">APELLIDOS</th>
						<th scope="col">RFC</th>
						<th scope="col">FECHA DE NACIMIENTO</th>
						<th scope="col">SEXO</th>
						<th scope="col">ESTADO CIVIL</th>
					</tr>
					<tr>
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/PN/C02"/>&nbsp;<xsl:value-of select="Reporte/PN/C03"/>
						</td>
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/PN/Paterno"/>&nbsp;<xsl:value-of select="Reporte/PN/C00"/>&nbsp;<xsl:value-of select="Reporte/PN/C01"/>
						</td>
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/PN/C05"/>
						</td>
						<td class="tbodyBC">
							<xsl:value-of select="substring(Reporte/PN/C04,1,2)"/>-<xsl:value-of select="substring(Reporte/PN/C04,3,2)"/>-<xsl:value-of select="substring(Reporte/PN/C04,5,4)"/>
						</td>
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/PN/C12"/>
						</td >
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/PN/C11"/>
						</td>
					</tr>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="7">RESUMEN DE REPORTE</th>
					</tr>
					<tr>
						<th>NUMERO</th>
						<th>FECHA DE INGRESO AL BURO</th>
						<th>CUENTAS TOTALES</th>
						<th>PAGOS FIJOS O HIPOTECARIOS</th>
						<th>REVOLVENTES</th>
						<th>CERRADAS</th>
						<th>
							TRANSALERT<br />Def RFC Dom.NC B.Com Dom.Inv
						</th>
					</tr>
					<xsl:for-each select="Reporte/RS">
						<tr>
							<td class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="substring(Fecha,1,2)"/>-<xsl:value-of select="substring(Fecha,3,2)"/>-<xsl:value-of select="substring(Fecha,5,4)"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C09"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C10"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C11"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C12"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C18"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="9">DOMICILIO(S) REPORTADO(S)</th>
					</tr>
					<tr>
						<th>NUM</th>
						<th>CALLE Y NUMERO</th>
						<th>COLONIA</th>
						<th>DEL/MPO</th>
						<th>CIUDAD</th>
						<th>ESTADO</th>
						<th>CP</th>
						<th>TEL</th>
						<th>REGISTRO EN BC</th>
					</tr>
					<xsl:for-each select="Reporte/PA">
						<tr>
							<td  class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="Direccion"/>&nbsp;<xsl:value-of select="C00"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C01"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C02"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C03"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C04"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C05"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="C07"/>
							</td>
							<td  class="tbodyBC">
								<xsl:value-of select="substring(C12,1,2)"/>-<xsl:value-of select="substring(C12,3,2)"/>-<xsl:value-of select="substring(C12,5,4)"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="12">DOMICILIO(S) DE EMPLEO(S) REPORTADO(S)</th>
					</tr>
					<tr>
						<th>NUM</th>
						<th>EMPRESA</th>
						<th>PUESTO</th>
						<th>SALARIO</th>
						<th>CALLEY Y NUM</th>
						<th>COLONIA</th>
						<th>DEL/MPO</th>
						<th>CIUDAD</th>
						<th>EDO</th>
						<th>CP</th>
						<th>TEL</th>
						<th>REGISTRO EN BC</th>
					</tr>
					<xsl:for-each select="Reporte/PE">
						<tr>
							<td  class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="Empresa"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C10"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C13"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C00"/>&nbsp;<xsl:value-of select="C01"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C02"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C03"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C04"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C05"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C06"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C07"/>
							</td>
							<td>
								<xsl:value-of select="substring(C17,1,2)"/>-<xsl:value-of select="substring(C17,3,2)"/>-<xsl:value-of select="substring(C17,5,4)"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="17">DETALLE DE LOS CREDITOS</th>
					</tr>
					<tr>
						<th class="thSBC">#</th>
						<th class="thSBC">TIPO</th>
						<th class="thSBC">
							OTORGANTE<BR />
							No.CUENTA
						</th>
						<th class="thSBC">MONEDA</th>
						<th class="thSBC">ACTUALIZADO</th>
						<th class="thSBC">APERTURA</th>
						<th class="thSBC">
							ULTIMO<BR />
							PAGO
						</th>
						<th class="thSBC">
							ULTIMA<BR />
							COMPRA
						</th>
						<th class="thSBC">CIERRE</th>
						<th class="thSBC">
							LIMITE DE<BR />
							CREDITO
						</th>
						<th class="thSBC">
							CREDITO<BR />
							MAXIMO
						</th>
						<th class="thSBC">
							SALDO<BR />
							ACTUAL
						</th>
						<th class="thSBC">
							SALDO<BR />
							VENCIDO
						</th>
						<th class="thSBC">
							MONTO A<BR />
							PAGAR
						</th>
            <th class="thSBC">
              FORMA DE <br />
              PAGO(MOP)
            </th>
						<th class="thSBC">HISTORICO</th>
						<th class="thSBC">OBS.</th>
					</tr>
					<xsl:for-each select="Reporte/TD">
						<tr>
							<td class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C06"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C02"/>
								<BR />
								<xsl:value-of select="C04"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C08"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(FechaAct,1,2)"/>-<xsl:value-of select="substring(FechaAct,3,2)"/>-<xsl:value-of select="substring(FechaAct,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(C13,1,2)"/>-<xsl:value-of select="substring(C13,3,2)"/>-<xsl:value-of select="substring(C13,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(C14,1,2)"/>-<xsl:value-of select="substring(C14,3,2)"/>-<xsl:value-of select="substring(C14,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(C15,1,2)"/>-<xsl:value-of select="substring(C15,3,2)"/>-<xsl:value-of select="substring(C15,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(C16,1,2)"/>-<xsl:value-of select="substring(C16,3,2)"/>-<xsl:value-of select="substring(C16,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C23"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C21"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C22"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C24"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C12"/>
								<br />
								<xsl:choose>
									<xsl:when test="C11 = 'B'"> Bimestral</xsl:when>
									<xsl:when test="C11 = 'Q'"> Trimestral</xsl:when>
									<xsl:when test="C11 = 'D'"> Diario</xsl:when>
									<xsl:when test="C11 = 'S'"> Quincenal</xsl:when>
									<xsl:when test="C11 = 'H'"> Por hora</xsl:when>
									<xsl:when test="C11 = 'W'"> Semanal</xsl:when>
									<xsl:when test="C11 = 'K'"> Catorcenal</xsl:when>
									<xsl:when test="C11 = 'Y'"> Anual</xsl:when>
									<xsl:when test="C11 = 'M'"> Mensual</xsl:when>
									<xsl:when test="C11 = 'P'"> Deducción del salario</xsl:when>
									<xsl:when test="C11 = 'Z'"> Pago mínimo</xsl:when>
								</xsl:choose>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C26"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C27"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C30"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="5">DETALLE DE CONSULTAS</th>
					</tr>
					<tr>
						<th>NUMERO</th>
						<th>OTORGANTE</th>
						<th>FECHA DE CONSULTA</th>
						<th>TIPO DE CONTRATO</th>
						<th>IMPORTE</th>
					</tr>
					<xsl:for-each select="Reporte/IQ">
						<tr>
							<td class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C02"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(Consulta,1,2)"/>-<xsl:value-of select="substring(Consulta,3,2)"/>-<xsl:value-of select="substring(Consulta,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:choose>
									<xsl:when test="C04 = 'AF'"> Aparatos /Muebles</xsl:when>
									<xsl:when test="C04 = 'AG'"> Agropecuario (PFAE)</xsl:when>
									<xsl:when test="C04 = 'AL'"> Arrendamiento Automotriz</xsl:when>
									<xsl:when test="C04 = 'AP'"> Aviación</xsl:when>
									<xsl:when test="C04 = 'AU'"> Compra de Automóvil</xsl:when>
									<xsl:when test="C04 = 'BD'"> Fianza</xsl:when>
									<xsl:when test="C04 = 'BT'"> Bote / Lancha</xsl:when>
									<xsl:when test="C04 = 'CC'"> Tarjeta de Crédito</xsl:when>
									<xsl:when test="C04 = 'CE'"> Cartas de Crédito (PFAE)</xsl:when>
									<xsl:when test="C04 = 'CF'"> Crédito fiscal</xsl:when>
									<xsl:when test="C04 = 'CL'"> Línea de Crédito</xsl:when>
									<xsl:when test="C04 = 'CO'"> Consolidación</xsl:when>
									<xsl:when test="C04 = 'CS'"> Crédito Simple (PFAE)</xsl:when>
									<xsl:when test="C04 = 'CT'"> Con Colateral (PFAE)</xsl:when>
									<xsl:when test="C04 = 'DE'"> Descuentos (PFAE)</xsl:when>
									<xsl:when test="C04 = 'EQ'"> Equipo</xsl:when>
									<xsl:when test="C04 = 'FI'"> Fideicomiso (PFAE)</xsl:when>
									<xsl:when test="C04 = 'FT'"> Factoraje</xsl:when>
									<xsl:when test="C04 = 'HA'"> Habilitación o Avío (PFAE)</xsl:when>
									<xsl:when test="C04 = 'HE'"> Préstamo tipo Home-Equity</xsl:when>
									<xsl:when test="C04 = 'HI'"> Mejoras a la casa</xsl:when>
									<xsl:when test="C04 = 'LS'"> Arrendamiento</xsl:when>
									<xsl:when test="C04 = 'MI'"> Otros</xsl:when>
									<xsl:when test="C04 = 'OA'"> Otros adeudos vencidos (PFAE)</xsl:when>
									<xsl:when test="C04 = 'PA'"> Préstamo para Personas Físicas con Actividad Empresarial (PFAE)</xsl:when>
									<xsl:when test="C04 = 'PB'"> Editorial</xsl:when>
									<xsl:when test="C04 = 'PG'"> PGUE (Préstamo como garantía de unidades industriales - PFAE)</xsl:when>
									<xsl:when test="C04 = 'PL'"> Préstamo personal</xsl:when>
									<xsl:when test="C04 = 'PR'"> Prendario (PFAE)</xsl:when>
									<xsl:when test="C04 = 'PQ'"> Quirografario (PFAE)</xsl:when>
									<xsl:when test="C04 = 'RC'"> Reestructurado (PFAE)</xsl:when>
									<xsl:when test="C04 = 'RD'"> Redescuento (PFAE)</xsl:when>
									<xsl:when test="C04 = 'RE'"> Bienes Raíces</xsl:when>
									<xsl:when test="C04 = 'RF'"> Refaccionario (PFAE)</xsl:when>
									<xsl:when test="C04 = 'RN'"> Renovado (PFAE)</xsl:when>
									<xsl:when test="C04 = 'RV'"> Vehículo Recreativo</xsl:when>
									<xsl:when test="C04 = 'SC'"> Tarjeta garantizada</xsl:when>
									<xsl:when test="C04 = 'SE'"> Préstamo garantizado</xsl:when>
									<xsl:when test="C04 = 'SG'"> Seguros</xsl:when>
									<xsl:when test="C04 = 'SM'"> Segunda hipoteca</xsl:when>
									<xsl:when test="C04 = 'ST'"> Préstamo para estudiante</xsl:when>
									<xsl:when test="C04 = 'TE'"> Tarjeta de Crédito Empresarial</xsl:when>
									<xsl:when test="C04 = 'UK'"> Desconocido</xsl:when>
									<xsl:when test="C04 = 'US'"> Préstamo no garantizado</xsl:when>
								</xsl:choose>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C06"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC" colspan="5">HAWK ALERTS</th>
					</tr>
					<tr>
						<th>NUMERO</th>
						<th>FECHA</th>
						<th>CODIGO</th>
						<th>NOMBRE</th>
						<th>DESCRIPCION</th>
					</tr>
					<xsl:for-each select="Reporte/HIR">
						<tr>
							<td class="tbodyBC">
								<xsl:value-of select="position()"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="substring(Fecha,1,2)"/>-<xsl:value-of select="substring(Fecha,3,2)"/>-<xsl:value-of select="substring(Fecha,5,4)"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C00"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C01"/>
							</td>
							<td class="tbodyBC">
								<xsl:value-of select="C02"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br />
				<table border="1" class="tableBC">
					<tr>
						<th scope="col" class="thTBC">DECLARACIONES DEL CONSUMIDOR</th>
					</tr>
					<tr>
						<td class="tbodyBC">
							<xsl:value-of select="Reporte/COMENTARIO"/>
						</td>
					</tr>
				</table>
				<br/>
        <table border="1" class="tableBC">
          <tr>
            <th scope="col" class="thTBC" colspan="7">SEGMENTO DE BS SCORE</th>
          </tr>
          <tr>            
            <th>NOMBRE DEL SCORE</th>
            <th>VALOR DEL SCORE</th>
            <th>CODIGO DE RAZON</th>
            <th>CODIGO DE RAZON</th>
            <th>CODIGO DE RAZON</th>
            <th>CODIGO DE ERROR</th>
          </tr>
          <xsl:for-each select="Reporte/SC">
            <tr>
              <td class="tbodyBC">
                <xsl:value-of select="C00"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C01"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C02"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C03"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C04"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C05"/>
              </td>
              <td class="tbodyBC">
                <xsl:value-of select="C06"/>
              </td>
              <!--<td class="tbodyBC">
                <xsl:value-of select="C07"/>
              </td>-->
            </tr>
          </xsl:for-each>
        </table>
        <br />
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>