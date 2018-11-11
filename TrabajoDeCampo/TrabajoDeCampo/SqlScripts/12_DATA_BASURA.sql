
--DOCENTES
INSERT INTO DOCENTE([DOC_APELLIDO],[DOC_NOMBRE],[DOC_DNI],[DOC_FECHA_NACIMIENTO],[DOC_DIRECCION],[DOC_TELEFONO]) VALUES('Haynes','Amery','34405165','2018-09-16','Ap #770-4578 Urna. Rd.','052-021-9791'),('Mays','Claire','6310496','2018-05-07','Ap #583-480 Malesuada. Rd.','035-207-6015'),('Leblanc','Leila','18588565','2019-11-03','5544 Erat Av.','047-150-9661'),('Washington','Hiram','40007659','2019-05-07','Ap #200-8782 Egestas. Ave','065-719-8978'),('Cobb','Renee','24859535','2018-04-05','5913 Fermentum Rd.','091-644-0904'),('Mason','Zeus','27605287','2019-01-22','1037 Commodo Av.','043-670-7722'),('Britt','Desiree','39151042','2019-04-02','P.O. Box 453, 8185 Gravida. St.','065-987-1485'),('Hess','Bernard','18982798','2018-04-03','Ap #239-3377 Nulla St.','099-810-0400'),('Mcfadden','Wendy','18662732','2019-11-10','8641 Arcu. Road','072-360-2919'),('Castillo','Jemima','48209620','2018-12-29','406 Quam Ave','077-456-1189');
INSERT INTO DOCENTE([DOC_APELLIDO],[DOC_NOMBRE],[DOC_DNI],[DOC_FECHA_NACIMIENTO],[DOC_DIRECCION],[DOC_TELEFONO]) VALUES('Saunders','Madonna','34317278','2018-03-28','762-2876 Lobortis Road','045-618-9800'),('Cameron','Timothy','10022907','2018-07-08','Ap #722-9202 Aliquet Av.','083-588-2694'),('Barnett','Emma','18633772','2018-05-05','158-1714 Mauris Ave','084-598-3799'),('Jordan','Alma','21941027','2019-11-10','Ap #854-5584 Ligula. Road','099-389-6300'),('Castillo','Chanda','46218359','2018-05-08','5077 Placerat. Rd.','093-460-8394'),('Cobb','Amir','6239377','2018-04-18','1988 Eros Ave','048-770-7522'),('Gilmore','Charlotte','11754309','2019-04-19','990-2475 Fermentum Av.','049-586-1060'),('Mclean','Isaac','41137868','2019-01-28','Ap #642-2978 Est, Street','092-810-4162'),('Shelton','Sybill','46968198','2018-09-08','Ap #933-2582 Curae; St.','080-457-7417'),('Cleveland','Jesse','19117832','2018-11-28','Ap #445-4641 Dis Ave','088-688-2331');


--MATERIAS

INSERT INTO MATERIA (MAT_NOMBRE,MAT_DESCRIPCION,MAT_TIPO) VALUES 
('LENGUA','MATERIA POR DEFECTO',1),
('SOCIALES','MATERIA POR DEFECTO',1),
('NATURALES','MATERIA POR DEFECTO',1),
('HISTORIA','MATERIA POR DEFECTO',1),
('QUIMICA','MATERIA POR DEFECTO',1)

-- MATERIA NIVEL
INSERT INTO MATERIA_NIVEL (MN_MATERIA_ID,MN_NIVEL_ID)
(1,3),
(2,3),
(3,3),
(4,3),
(5,3)

--TUTOR
INSERT INTO TUTOR (TUT_NOMBRE,TUT_APELLIDO,TUT_TELEFONO_PRIMARIO,TUT_DNI,TUT_EMAIL)
VALUES('Esteban','Perez','mz+eYcY4K6kiUwE4rE/V18I8TzQDoxoF/FEuX733S08=','5566448844','tutor@email.com');

--CURSOS
INSERT INTO CURSO(CUR_NIVEL_ID,CUR_CAPACIDAD,CUR_CODIGO,CUR_TURNO,CUR_LETRA)
VALUES (3,10,'3ESBMA','M','A')
--ALUMNOS
INSERT INTO ALUMNO([alu_apellido],[alu_nombre],[alu_fecha_nacimiento],[alu_domicilio],[alu_curso],[ALU_DNI]) VALUES('Cote','Mariam','2005-11-22','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'12297867'),('Flynn','Aquila','2008-01-10','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'49677262'),('Dixon','Dustin','2006-05-03','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'15757163'),('Mcgee','Amelia','2007-08-09','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'22587121'),('Sparks','Camilla','2005-05-04','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'35611827'),('Bell','Xenos','2008-10-15','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'6134011'),('Webster','Abel','2006-05-07','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'35558854'),('Thompson','Hadassah','2007-12-09','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'32832521'),('Saunders','Kameko','2007-03-23','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'23863579'),('Bond','Avye','2008-07-19','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'13973511');
INSERT INTO ALUMNO([alu_apellido],[alu_nombre],[alu_fecha_nacimiento],[alu_domicilio],[alu_curso],[ALU_DNI]) VALUES('Figueroa','Dieter','2008-02-10','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'24728340'),('Lindsay','Oliver','2005-06-26','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'10895226'),('Larsen','Ronan','2005-05-04','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'45944715'),('Hall','Preston','2007-05-07','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'47895899'),('Bennett','Adele','2006-07-18','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'20200543'),('Bernard','Harrison','2006-03-04','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'42786127'),('Kim','Lucius','2006-10-27','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'32071794'),('Mckay','Leila','2006-01-15','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'36481331'),('Hampton','Alvin','2005-09-01','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'11989060'),('Davidson','Cody','2006-03-11','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'18505753'),
('Charlotte','Cannigia','2006-03-11','rAsYNCowFc+3VntxSh0+jTr02cIDIffnbo+UeOsuqZQ=',1,'123456788');


--INSERT INTO ALUMNO TUTOR
INSERT INTO ALUMNO_TUTOR (AT_ALUMNO_ID,AT_TUTOR_ID)
(1,1),
(2,1),
(3,1),
(4,1),
(5,1),
(6,1),
(7,1),
(8,1),
(9,1),
(10,1),
(11,1),
(12,1),
(13,1),
(14,1),
(15,1),
(16,1),
(17,1),
(18,1),
(19,1),
(20,1),
(21,1);


--PLANILLAS DE EVALUACION

insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(1,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(1,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(1,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(1,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(1,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(2,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(2,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(2,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(2,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(2,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(3,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(3,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(3,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(3,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(3,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(4,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(4,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(4,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(4,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(4,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(5,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(5,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(5,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(5,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(5,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(6,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(6,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(6,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(6,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(6,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(7,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(7,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(7,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(7,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(7,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(8,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(8,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(8,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(8,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(8,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(9,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(9,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(9,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(9,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(9,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(10,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(10,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(10,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(10,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(10,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)

insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(11,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(11,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(11,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(11,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(11,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(12,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(12,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(12,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(12,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(12,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(13,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(13,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(13,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(13,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(13,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(14,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(14,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(14,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(14,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(14,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(15,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(15,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(15,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(15,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(15,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(16,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(16,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(16,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(16,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(16,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(17,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(17,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(17,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(17,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(17,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(18,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(18,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(18,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(18,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(18,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(19,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(19,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(19,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(19,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(19,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)


insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(20,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',1),
(20,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',2),
(20,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',3),
(20,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',4),
(20,3,'JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=','JnM8eN8xTUptU+2jwtnRntwvFEwKpt7+CLKqXmZjEvc=',1,'',5)

--ALUMNO 21 DESAPROBADO
insert into planilla_de_evaluacion 
(pde_alumno_id,pde_nivel_id,pde_trimestre_1,pde_trimestre_2,pde_trimestre_3,pde_nota_final,pde_condicion,pde_dvh,pde_materia_id)
VALUES
(21,3,'gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=',0,'',1),
(21,3,'gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=',0,'',2),
(21,3,'gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=',0,'',3),
(21,3,'gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=',0,'',4),
(21,3,'gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=','gwA3JRcXygziOugkhtQUgBFEuyzgoRmhfj6RkKKTyWE=',0,'',5)



--INSERTO INASISTENCIAS PARA EL PRIMER ALUMNO
insert into inasistencia_de_alumno
(ina_alumno_id,ina_fecha,ina_valor,ina_dvh,ina_justificada)
values(1,GETDATE(),1,'',0),
values(1,GETDATE()-1,1,'',0),
values(1,GETDATE()-2,1,'',0),
values(1,GETDATE()-3,1,'',0),
values(1,GETDATE()-4,1,'',0),
values(1,GETDATE()-5,1,'',0),
values(1,GETDATE()-6,1,'',0),
values(1,GETDATE()-7,1,'',0),
values(1,GETDATE()-8,1,'',0),
values(1,GETDATE()-9,1,'',0),
values(1,GETDATE()-10,1,'',0),
values(1,GETDATE()-11,1,'',0),
values(1,GETDATE()-12,1,'',0),
values(1,GETDATE()-13,1,'',0),
values(1,GETDATE()-14,1,'',0),
values(1,GETDATE()-15,1,'',0),
values(1,GETDATE()-16,1,'',0),
values(1,GETDATE()-17,1,'',0),
values(1,GETDATE()-18,1,'',0);
values(1,GETDATE()-19,1,'',0);
values(1,GETDATE()-20,1,'',0);