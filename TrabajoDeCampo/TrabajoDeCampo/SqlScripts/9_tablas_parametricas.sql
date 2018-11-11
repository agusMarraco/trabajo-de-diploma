insert into IDIOMA(IDI_CODIGO,IDI_DESCRIPCION) values ('es','español');
insert into IDIOMA(IDI_CODIGO,IDI_DESCRIPCION) values ('en','ingles');

insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('USUARIO','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('PATENTE','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('BITACORA','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('PLANILLA_DE_EVALUACION','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('FAMILIA','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('INASISTENCIA_DE_ALUMNO','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('AMONESTACION','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('USUARIO_FAMILIA','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('USUARIO_PATENTE','')
insert into digito_vertical (dv_nombre_tabla,dv_digito_calculado) values ('FAMILIA_PATENTE','')



insert into CRITICIDAD (CRIT_NOMBRE) values ('ALTA')
insert into CRITICIDAD (CRIT_NOMBRE) values ('MEDIA')
insert into CRITICIDAD (CRIT_NOMBRE) values ('BAJA')

INSERT INTO ORIENTACION (ORI_NOMBRE) VALUES ('NATURALES')
INSERT INTO ORIENTACION (ORI_NOMBRE) VALUES ('ECONOMÍA')

INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('1ESB','Primero ESB',null)
INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('2ESB','Segundo ESB',null)
INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('3ESB','Tercero ESB',null)

INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('4ESBN','Cuarto ESB',1)
INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('4ESBE','Cuarto ESB',2)

INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('5ESBN','Quinto ESB',1)
INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('5ESBE','Quinto ESB',2)

INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('6ESBN','Sexto ESB',1)
INSERT INTO NIVEL(NIV_CODIGO,NIV_DESCRIPCION,NIV_ORIENTACION) VALUES ('6ESBE','Sexto ESB',2)


INSERT INTO MODULO (MOD_HORA_INICIO,MOD_HORA_FINAL) VALUES('08:00:00','10:00:00');
INSERT INTO MODULO (MOD_HORA_INICIO,MOD_HORA_FINAL) VALUES('10:00:00','12:00:00');
INSERT INTO MODULO (MOD_HORA_INICIO,MOD_HORA_FINAL) VALUES('12:00:00','14:00:00');
INSERT INTO MODULO (MOD_HORA_INICIO,MOD_HORA_FINAL) VALUES('14:00:00','16:00:00');
INSERT INTO MODULO (MOD_HORA_INICIO,MOD_HORA_FINAL) VALUES('16:00:00','18:00:00');

INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('Jk6McJck7YwXp9AODP/8cZoB7BKrzmO9LEDzEwNGREU=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('uXd6cYoc/qSWgqNwB+yyOoDdqHQiaIMnBDW2Xu/Th523chKz1+4v40PvisBgAMWr','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('3WKGu+RIlPDe134pycybo2OElPIYSfcS45FIfgvN6LIgEZqhWfv4LO5DL0W3X8wH','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('I0ybz1x74OfxKv205hB86+1kQ6nfvYPYPoXXYxlRwKLIMFOYmViREnMTyHFftMKJ','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('DEcy4OcxcjRh01kLhtmu6qaKoGeUes5ESq0mxjRIO4Km+uzFVMHR9sz+4c4WOMlN','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('T7P9qAWW88xf9AmZ0L9IKhh7WQMFK4Gh0YGpuwphtaVlAOgm2AL7MQd/SA4kRXJO','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('PxKo1YRjI3a6mESJXPsgQnNq17Y8z4cYZm4q2f5vGjc=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('gbHVNVs1t661r83Bni4WESDEdXCVPmQFM8mxBnCgwog=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('9ZEHBZW7qpb1d1ylADgY20JZ/LKdgKZ9CpuyJiZnSJQ=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('ZzDOBwFOhAE8DOXS9wjPMyu1uQpVgW+kpJCO3y98ebI=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('7hWRsjT2tNMT/3AbKXOo0wLcPJ2s9VV3rGp1WHBdtsU=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('4FhnO7SpZssZtl5ClZDTQqzMGm0OepChSq6NhHks9Il9MEO4n3mtFwjumC7reK8b','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('G9rebK2tzhy72Du3zokm6EQ2fBnztTm9S6LqVs4GwhEcfhITaNS9+mEbbxOkyG+e','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('B+omYqPaaxcuL6kDZrcG04CfQmrR9jwPyksV2E7G8h0=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('GT3Y4X5tGf4jm2bD72NugS0yQJ30cpIthE0HLkUAw+lVCseE2iqPhebxS7oMpIGW','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('tR5t6aYXcf6XIlFddn020iI2rdrA/LhJCqNd7U0BQ+U=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('Po0AI6B8GHcnHbhBEqfHJLwGMiMcUwm4f0bvqHEClrYA9oFPCdBHE1J2EgNtElcT','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('scwVmJgFw1zzEkZgjLEdSP1GkpBR4oXDtJPImLzFFxRgUNnPqfgRtCny4+1BkyZCRAYOib0z5YqZK/BBSFpWgw==','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('KkI71emXEC0UR1fmMSqiHCqd3XhjeVViXkA4jS7hyu4=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('0Op32EFwAU5Uo6oC+LYpMDFYrkay2aSQyWsHhrNUO27yQUUvcRrglL7ramXoTJyt','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('fukI5IC9WoI4bYAH4oEI5J8a2/p29OPwzo4LaHZUyfpySaATpKjlMAFFE2BYvc8C','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('9Giq3BaRf5aaEwdYsm2s8i2W5x4xdlMmBWAOndZnSDA=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('yaRphrDJ1qZ2hxnJ6bU90Qu1Uz3L7aSViG1iyHgLL5g=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('ni2Ay9NQVy/CsEiUoXJWzN31uOHER03OWZ5GR3QATPA=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('+lBBDqjNdI3Hny91/w3iOgYJ+PnhQ2PmA64mhnqt4ck=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('RYCBTHwIZSKwQ4jhmZhZT3EutMr92WiHaLAGe34z1zc=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('0FirIgwUuIz3UEJu52LYyHzzCtBS+rqiNWtNZLw/aAnR9FVb3w5R1HxBEf95RoxP','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('P/1vAK6pXoRP+NEIYFP4wMqKVgEhyJEvclv5MWonHSI=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('yKOjlCJ8IgH7bDgt3PBCDbt0UOkfsaWTvR6dJi8jaS0=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('Bje9+4JN8E+H1xjyn0Q8reBcFmeqHfOpvLaWS6wyQx0=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('YP2KgTkhFtqEINzjrppBBLQapSAdZjxdiajT7oRWYsg=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('/OOHNlHWD9sX9HZQIT36lp/vKYwzOPvmiBORCticnts=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('vtL6f2tjCYFWQwQhsPHSz+QSbkO4/taIT/KmqyRa9cWdAZu/4sCMO63OxKPzv86P','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('asQ26jEezwePU2l+hHJYFQ3HstO/mf6IgRzn27uDZiQ=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('L3lmiKwQCE7szYNndR4cPbFG7o7jtg/4wy8sL80Vc20f4kysNhLwwiYYVnob8tgc','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('LSijAizekUvsUfKCJfyDevGhjSnXj6BI4lhMsBJOSvU=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('NGA83IXJKn4TPiFHiwyH2jVk7qE9cpQ0P9eoSmiDVi0=','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('fONlQoGgYh2lrYzVS1qiOZP+OzW46fjxjBRa8ft5tU5T+H+s10ZH8kOH71NHS4Pu','')
INSERT [dbo].[PATENTE] ([PAT_DESC],[PAT_DVH]) VALUES ('iwyL9uQvb75IapS2+yGIikLSVbn0A9bvNOIBwQYemCL5d6pAQa/eixCR2gPOP/tD','')
