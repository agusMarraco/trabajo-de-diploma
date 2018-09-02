﻿use master

CREATE DATABASE TRABAJO_DIPLOMA

GO

USE TRABAJO_DIPLOMA

GO


CREATE TABLE TUTOR(
	TUT_ID BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	TUT_NOMBRE VARCHAR(30) NOT NULL,
	TUT_APELLIDO VARCHAR(30) NOT NULL,
	TUT_TELEFONO_PRIMARIO NVARCHAR(200) NOT NULL,
	TUT_TELEFONO_SECUNDARIO NVARCHAR(200),
	TUT_DNI VARCHAR(30) NOT NULL UNIQUE,
	TUT_EMAIL VARCHAR(30) NOT NULL

)

CREATE TABLE MATERIA(
	MAT_ID BIGINT PRIMARY KEY NOT NULL IDENTITY (1,1),
	MAT_NOMBRE VARCHAR(20) NOT NULL UNIQUE,
	MAT_DESCRIPCION VARCHAR(50) NOT NULL,
	MAT_TIPO BIT NOT NULL,
	MAT_BORRADA BIT 
)

CREATE TABLE ORIENTACION(
	ORI_CODIGO BIGINT PRIMARY KEY IDENTITY(1,1),
	ORI_NOMBRE VARCHAR(50) NOT NULL
)
CREATE TABLE NIVEL(
	NIV_ID BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
	NIV_CODIGO VARCHAR(10) NOT NULL UNIQUE,
	NIV_DESCRIPCION VARCHAR(20) NOT NULL,
	NIV_ORIENTACION BIGINT FOREIGN KEY REFERENCES ORIENTACION(ORI_CODIGO)
)
CREATE TABLE MATERIA_NIVEL(
	MN_MATERIA_ID BIGINT FOREIGN KEY REFERENCES MATERIA(MAT_ID) NOT NULL,
	MN_NIVEL_ID BIGINT FOREIGN KEY REFERENCES NIVEL(NIV_ID) NOT NULL
)
CREATE TABLE DOCENTE(
	DOC_LEGAJO BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DOC_APELLIDO VARCHAR(20)  NOT NULL,
	DOC_NOMBRE VARCHAR(20)  NOT NULL,
	DOC_DNI VARCHAR(20)  NOT NULL UNIQUE,
	DOC_FECHA_NACIMIENTO DATE NOT NULL,
	DOC_DIRECCION VARCHAR(50)  NOT NULL,
	DOC_TELEFONO VARCHAR(20)  NOT NULL,
)
CREATE TABLE INASISTENCIA_DOCENTE(
	IND_DOCENTE_ID BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	IND_FECHA DATE NOT NULL,
	IND_DVH VARCHAR(50) NOT NULL,
	IND_JUSTIFICADA BIT NOT NULL
)

CREATE TABLE MODULO(
	MOD_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	MOD_HORA_INICIO VARCHAR(10) NOT NULL,
	MOD_HORA_FINAL VARCHAR(10) NOT NULL
)

CREATE TABLE CURSO(
	CUR_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	CUR_NIVEL_ID BIGINT FOREIGN KEY REFERENCES NIVEL(NIV_ID) NOT NULL,
	CUR_CAPACIDAD BIGINT NOT NULL,
	CUR_CODIGO VARCHAR(10) NOT NULL UNIQUE,
	CUR_TURNO CHAR(1) NOT NULL,
	CUR_LETRA CHAR(1) NOT NULL,
	CUR_BORRADO BIT
)
CREATE TABLE HORARIO(
	HOR_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	HOR_DIA BIGINT NOT NULL,
	HOR_CURSO BIGINT FOREIGN KEY REFERENCES CURSO(CUR_ID) NOT NULL,
	HOR_MATERIA_ID BIGINT FOREIGN KEY REFERENCES MATERIA(MAT_ID) NOT NULL,
	HOR_DOCENTE_ID BIGINT FOREIGN KEY REFERENCES DOCENTE(DOC_LEGAJO) NOT NULL,
	HOR_MODULO_ID BIGINT FOREIGN KEY REFERENCES MODULO(MOD_ID)
)
CREATE TABLE ALUMNO(
	ALU_LEGAJO BIGINT PRIMARY KEY IDENTITY(1,1),
	ALU_APELLIDO VARCHAR(30) NOT NULL,
	ALU_NOMBRE VARCHAR(30) NOT NULL,
	ALU_FECHA_NACIMIENTO DATE NOT NULL,
	ALU_DNI VARCHAR(30) NOT NULL UNIQUE,
	ALU_CURSO BIGINT FOREIGN KEY REFERENCES CURSO(CUR_ID) NOT NULL,
	ALU_DOMICILIO NVARCHAR(150) NOT NULL,
	ALU_ORIENTACION BIGINT FOREIGN KEY REFERENCES ORIENTACION(ORI_CODIGO),
	ALU_BORRADO BIT
)
CREATE TABLE INASISTENCIA_DE_ALUMNO(
	INA_ALUMNO_ID BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	INA_FECHA DATE NOT NULL,
	INA_VALOR NUMERIC(2,2) NOT NULL,
	INA_DVH VARCHAR(50) NOT NULL,
	INA_JUSTIFICADA BIT NOT NULL
)
CREATE TABLE AMONESTACION(
	AMON_ALUMNO_ID BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	AMON_FECHA DATE NOT NULL,
	AMON_MOTIVO TEXT NOT NULL,
	AMON_DVH VARCHAR(50) NOT NULL
)
CREATE TABLE ALUMNO_TUTOR(
	AT_ALUMNO_ID BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	AT_TUTOR_ID BIGINT FOREIGN KEY REFERENCES TUTOR(TUT_ID) NOT NULL
)
CREATE TABLE NOTA(
	NOTA_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	NOTA_MATERIA_ID BIGINT FOREIGN KEY REFERENCES MATERIA(MAT_ID) NOT NULL,
	NOTA_DOCENTE_ID BIGINT FOREIGN KEY REFERENCES DOCENTE(DOC_LEGAJO) NOT NULL,
	NOTA_TRIMESTRE BIGINT NOT NULL,
	NOTA_NOTA BIGINT NOT NULL,
	NOTA_CURSO_ID BIGINT FOREIGN KEY REFERENCES CURSO(CUR_ID) NOT NULL,
	NOTA_ALUMNO_ID BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	NOTA_DVH VARCHAR(50) NOT NULL,
	NOTA_BORRADO BIT NOT NULL DEFAULT 0,
)
CREATE TABLE NOTA_DE_MESA(
	NDM_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	NDM_DOCENTE_ID BIGINT FOREIGN KEY REFERENCES DOCENTE(DOC_LEGAJO) NOT NULL,
	NDM_ADJUNTO_ID BIGINT FOREIGN KEY REFERENCES DOCENTE(DOC_LEGAJO) NOT NULL,
	NDM_ALUMNO BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	NDM_CURSO_ID BIGINT FOREIGN KEY REFERENCES CURSO(CUR_ID) NOT NULL,
	NDM_FECHA DATE NOT NULL,
	NDM_LIBRO VARCHAR(10) NOT NULL,
	NDM_FOLIO VARCHAR(10) NOT NULL,
	NDM_NOTA BIGINT NOT NULL,
	NDM_MATERIA_ID BIGINT FOREIGN KEY REFERENCES MATERIA(MAT_ID),
	NDM_DVH VARCHAR(50) NOT NULL
)
CREATE TABLE PLANILLA_DE_EVALUACION(
	PDE_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	PDE_ALUMNO_ID BIGINT FOREIGN KEY REFERENCES ALUMNO(ALU_LEGAJO) NOT NULL,
	PDE_NIVEL_ID BIGINT FOREIGN KEY REFERENCES NIVEL(NIV_ID) NOT NULL,
	PDE_TRIMESTRE_1 NVARCHAR(50) ,
	PDE_TRIMESTRE_2 NVARCHAR(50) ,
	PDE_TRIMESTRE_3 NVARCHAR(50) ,
	PDE_NOTA_FINAL NVARCHAR(50) ,
	PDE_CONDICION BIGINT ,
	PDE_DVH VARCHAR(50) NOT NULL,
	PDE_MATERIA_ID BIGINT FOREIGN KEY REFERENCES MATERIA(MAT_ID)
)

CREATE TABLE DIGITO_VERTICAL(
	DV_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	DV_NOMBRE_TABLA VARCHAR(50) NOT NULL,
	DV_DIGITO_CALCULADO VARCHAR(50) NOT NULL
)

CREATE TABLE IDIOMA(
	IDI_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	IDI_CODIGO 	VARCHAR(10) NOT NULL,
	IDI_DESCRIPCION VARCHAR(20) NOT NULL
)

CREATE TABLE USUARIO(
	USU_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	USU_DNI VARCHAR(20) NOT NULL UNIQUE,
	USU_EMAIL VARCHAR(50) NOT NULL UNIQUE,
	USU_PASS VARCHAR(50) NOT NULL,
	USU_ALIAS NVARCHAR(300) NOT NULL,
	USU_INTENTOS BIGINT NOT NULL DEFAULT 0,
	USU_BAJA BIT DEFAULT 0 NOT NULL,
	USU_NOMBRE VARCHAR(20) NOT NULL,
	USU_APELLIDO VARCHAR(20) NOT NULL,
	USU_DIRECCION VARCHAR(50) NOT NULL,
	USU_TELEFONO VARCHAR(20) NOT NULL,
	USU_DVH VARCHAR(50) NOT NULL,
	USU_IDIOMA BIGINT FOREIGN KEY REFERENCES IDIOMA(IDI_ID)
)

CREATE TABLE MENSAJE(
	MSJ_CODIGO VARCHAR(50) NOT NULL,
	MSJ_IDIOMA_ID BIGINT FOREIGN KEY REFERENCES IDIOMA(IDI_ID),
	MSJ_TEXTO VARCHAR(50)
)

CREATE TABLE FAMILIA(
	FAM_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	FAM_NOMBRE NVARCHAR(300) NOT NULL,
	FAM_BLOQUEADA BIT NOT NULL DEFAULT 0,
	FAM_DVH VARCHAR(50) NOT NULL
)

CREATE TABLE PATENTE(
	PAT_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	PAT_DESC NVARCHAR(300) NOT NULL,
	PAT_DVH VARCHAR(50) NOT NULL
)

CREATE TABLE USUARIO_PATENTE(
	UP_USUARIO_ID BIGINT FOREIGN KEY REFERENCES USUARIO(USU_ID),
	UP_PATENTE_ID BIGINT FOREIGN KEY REFERENCES PATENTE(PAT_ID),
	UP_DVH VARCHAR(50) NOT NULL,
	UP_BLOQUEADA BIT DEFAULT 0
)
CREATE TABLE USUARIO_FAMILIA(
	UF_USUARIO_ID BIGINT FOREIGN KEY REFERENCES USUARIO(USU_ID),
	UF_FAMILIA_ID BIGINT FOREIGN KEY REFERENCES FAMILIA(FAM_ID)
)

CREATE TABLE CRITICIDAD(
	CRIT_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	CRIT_NOMBRE VARCHAR(50) NOT NULL
)

CREATE TABLE BITACORA(
	BIT_ID BIGINT PRIMARY KEY IDENTITY(1,1),
	BIT_USUARIO BIGINT FOREIGN KEY REFERENCES USUARIO(USU_ID),
	BIT_MENSAJE TEXT NOT NULL,
	BIT_CRITICIDAD_ID BIGINT FOREIGN KEY REFERENCES CRITICIDAD(CRIT_ID),
	BIT_FECHA DATE NOT NULL,
	BIT_DVH VARCHAR(50) NOT NULL
)

CREATE TABLE FAMILIA_PATENTE(
	FP_PATENTE_ID BIGINT FOREIGN KEY REFERENCES PATENTE(PAT_ID),
	FP_FAMILIA_ID BIGINT FOREIGN KEY REFERENCES FAMILIA(FAM_ID)
)

