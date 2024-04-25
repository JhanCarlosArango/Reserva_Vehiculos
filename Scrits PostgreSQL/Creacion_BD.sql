create database alquiler;

\c alquiler;

create table usuario (
    id_usuario serial,
    USUARIO VARCHAR(40),
    CONTRASENIA VARCHAR(40)
);

create table persona (
    id_persona INT NOT NULL
);

--------------------------------------------
create table persona(
num_documento int not null,
primer_nombre varchar(45),
segundo_nombre varchar(45),
primer_apellido varchar(45),
segundo_apellido varchar(45),
num_telefonico int
);

create table (
correo varchar(45)
);


create table usuario_rol(
);

create table rol(
id_rol serial not null,
tipo_rol varchar(45)
);

create table reserva(
id_reserva serial not null,
acep_fecha date,
estado_reserva varchar(20) default 'activa',
valor_reserva decimal(10,2),

);

create table pet_reserva(
id_pet_reserva serial not null,
fecha_ini DATE,
fecha_fin DATE,
hora_ini varchar(20),
hora_fin varchar(20),
estado varchar(15) not null default 'wait',
costo decimal(10,2)
);


create table acuerdo(
id_acuerdo int not null,
fecha_inicio date,
fecha_fin date
);

create table vehiculo(
num_placa int not null,
aire_acondicionado varchar(45),
capacidad_pasajeros int(10),
capacidad_carga decimal(10,2)
);

create table tipo_direccion(
id_tipo_direccion int not null,
tipo_direccion varchar(45)
);

create table estado_vehiculo(
id_estado_vehiculo int not null,
estado varchar(45)
);

create table categoria(
id_categoria serial not null,
tipo_vehiculo varchar(45),
costo decimal(10,2)
);

create table caja_cambios(
id_caja_cambios int not null,
tipo_caja_cambios varchar(45)
);

create table marca(
id_marca int not null,
nombre_marca varchar(45)
);

create table espec_vehiculo(
id_espec_vehiculo int not null,
modelo int,
color varchar(45),
num_chasis decimal(10,10),
modelo_motor int,
cilindraje int
);

create table tipo_combustible(
id_tipo_combustible int not null,
tipo_combustible varchar(45)
);

create table vehiculo_doc_legal(
fecha_doc date,
num_doc int
);

create table doc_legal(
id_doc_legal int not null,
tipo_doc varchar(45)
);

alter table persona add constraint pk_num_documento primary key(num_documento);---
alter table usuario add constraint pk_id_usuario primary key(id_usuario);
alter table rol add constraint pk_id_rol primary key(id_rol);
alter table reserva add constraint pk_id_reserva primary key(id_reserva);
alter table pet_reserva add constraint pk_id_pet_reserva primary key(id_pet_reserva);
alter table acuerdo add constraint pk_id_acuerdo primary key(id_acuerdo);
alter table vehiculo add constraint pk_num_placa primary key(num_placa);
alter table tipo_direccion add constraint pk_id_tipo_direccion primary key(id_tipo_direccion);
alter table estado_vehiculo add constraint pk_id_estado_vehiculo primary key(id_estado_vehiculo);
alter table categoria add constraint pk_id_categoria primary key(id_categoria);
alter table caja_cambios add constraint pk_id_caja_cambios primary key(id_caja_cambios);
alter table marca add constraint pk_id_marca primary key(id_marca);
alter table espec_vehiculo add constraint pk_id_espec_vehiculo primary key(id_espec_vehiculo);
alter table tipo_combustible add constraint pk_id_tipo_combustible primary key(id_tipo_combustible);
alter table doc_legal add constraint pk_id_doc_legal primary key(id_doc_legal);


alter table prestador_vehiculo add prestador_num_documento int;
alter table usuario_rol add fk_id_usuario int;
alter table usuario_rol add fk_id_rol int;
alter table vehiculo_doc_legal add fk_num_placa int;
alter table vehiculo_doc_legal add fk_id_doc_legal int;
alter table usuario add fk_num_documento int;--
alter table pet_reserva add fk_id_categoria int;
alter table pet_reserva add fk_id_usuario int;
alter table reserva add fk_id_pet_reserva int;
alter table reserva add fk_num_placa int;
alter table acuerdo add fk_prestador_num_documento int;
alter table acuerdo add fk_num_placa int;
alter table vehiculo add fk_id_marca int;
alter table vehiculo add fk_id_estado_vehiculo int;
alter table vehiculo add fk_id_categoria int;
alter table vehiculo add fk_id_tipo_direccion int;
alter table vehiculo add fk_id_caja_cambios int;
alter table vehiculo add fk_id_espec_vehiculo int;
alter table espec_vehiculo add fk_id_tipo_combustible int;
alter table vehiculo_doc_legal add fk_num_placa int;
alter table vehiculo_doc_legal add fk_id_doc_legal int;
alter table usuario_rol add pk_usuario_rol int;



alter table usuario_rol add constraint fk_id_usuario foreign key (fk_id_usuario) references usuario (id_usuario);
alter table usuario_rol add constraint fk_id_rol foreign key (fk_id_rol) references rol (id_rol);
alter table vehiculo_doc_legal add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table vehiculo_doc_legal add constraint fk_id_doc_legal foreign key (fk_id_doc_legal) references doc_legal (id_doc_legal);
alter table usuario add constraint fk_num_documento foreign key (fk_num_documento) references persona (num_documento);--
alter table pet_reserva add constraint fk_id_categoria foreign key (fk_id_categoria) references categoria (id_categoria);
alter table pet_reserva add constraint fk_id_usuario foreign key (fk_id_usuario) references usuario (id_usuario);
alter table reserva add constraint fk_id_pet_reserva foreign key (fk_id_pet_reserva) references pet_reserva (id_pet_reserva);
alter table reserva add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table acuerdo add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table vehiculo add constraint fk_id_marca foreign key (fk_id_marca) references marca (id_marca);
alter table vehiculo add constraint fk_id_estado_vehiculo foreign key (fk_id_estado_vehiculo) references estado_vehiculo (id_estado_vehiculo);
alter table vehiculo add constraint fk_id_categoria foreign key (fk_id_categoria) references categoria (id_categoria);
alter table vehiculo add constraint fk_id_tipo_direccion foreign key (fk_id_tipo_direccion) references tipo_direccion (id_tipo_direccion);
alter table vehiculo add constraint fk_id_caja_cambios foreign key (fk_id_caja_cambios) references caja_cambios (id_caja_cambios);
alter table vehiculo add constraint fk_id_espec_vehiculo foreign key (fk_id_espec_vehiculo) references espec_vehiculo (id_espec_vehiculo);
alter table espec_vehiculo add constraint fk_id_tipo_combustible foreign key (fk_id_tipo_combustible) references tipo_combustible (id_tipo_combustible);
alter table vehiculo_doc_legal add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table vehiculo_doc_legal add constraint fk_id_doc_legal foreign key (fk_id_doc_legal) references doc_legal (id_doc_legal);

---- primaria compuesta
alter table usuario_rol add constraint pk_usuario_rol primary key(fk_id_usuario, fk_id_rol);



---se debe porganizar que va primero

INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Automóvil', 93000);
INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Camióneta', 123000);
INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Motocicleta Baja CC', 45000);
INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Motocicleta Media CC', 70000);
INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Motocicleta Alta CC', 100000);
INSERT INTO categoria (tipo_vehiculo, costo) VALUES ('Depotivo', 320000);

INSERT INTO pet_reserva (fecha_ini, fecha_fin, hora_ini, hora_fin)
VALUES ('2024-05-01', '2024-05-01', '10:00:00', '12:00:00');

insert into rol(tipo_rol)values('Administrador');
insert into rol(tipo_rol)values('Proveerdor');
insert into rol(tipo_rol)values('Cliente');

insert into usuario_rol (fk_id_usuario,fk_id_rol) values(1,1);
insert into usuario_rol (fk_id_usuario,fk_id_rol) values(2,2);
insert into usuario_rol (fk_id_usuario,fk_id_rol) values(2,3);

INSERT INTO persona (num_documento, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, num_telefonico) 
VALUES (117987265, 'Jhan', 'Carlos', 'Arango', 'Usuga', 321490569);
INSERT INTO persona (num_documento, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, num_telefonico) 
VALUES (117987265, 'Jhan', 'Carlos', 'Arango', 'Usuga', 112);

insert into usuario(usuario,contrasenia) values('arango','root',117987265);
insert into usuario(usuario,contrasenia) values('prueba','root',112);