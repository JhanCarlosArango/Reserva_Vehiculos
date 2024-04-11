create table usuario (
    id_usuario serial,
    USUARIO VARCHAR(40),
    CONTRASENIA VARCHAR(40)
);

create table pet_reserva (
    id_pet_reserva INT NOT NULL
);

create table persona (
    id_persona INT NOT NULL
);

create table cliente (
    id_cliente INT NOT NULL
);
create table vehiculo(
    id_vehiculo int not null
);
--------------------------------------------
create database alquiler;

\c alquiler;

create table persona(
num_documento int not null,
primer_nombre varchar(45),
segundo_nombre varchar(45),
primer_apellido varchar(45),
segundo_apellido varchar(45),
num_telefonico int
);

create table cliente(
correo varchar(45)
);

create table administrador(
correo varchar(45)
);

create table prestador_vehiculo(
correo varchar(45)
);

create table usuario(
id_usuario int not null,
usuario varchar(45),
contrasena varchar(45)
);

create table usuario_rol(
);

create table rol(
id_rol int not null,
tipo_rol varchar(45)
);

create table reserva(
id_reserva int not null,
fecha_reserva date,
estado_reserva varchar(45),
costo decimal(10,2)
);

create table pet_reserva(
id_pet_reserva int not null,
estado varchar(45)
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
id_categoria int not null,
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

alter table persona add constraint pk_num_documento primary key(num_documento);
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

alter table cliente add cliente_num_documento int;
alter table prestador_vehiculo add prestador_num_documento int;
alter table administrador add administrador_num_documento int;
alter table usuario_rol add fk_id_usuario int;
alter table usuario_rol add fk_id_rol int;
alter table vehiculo_doc_legal add fk_num_placa int;
alter table vehiculo_doc_legal add fk_id_doc_legal int;
alter table usuario add fk_num_documento int;
alter table usuario add fk_administrador_num_documento int;
alter table pet_reserva add fk_administrador_num_documento int;
alter table pet_reserva add fk_id_categoria int;
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

alter table cliente add constraint cliente_num_documento foreign key (cliente_num_documento) references persona (num_documento);
alter table prestador_vehiculo add constraint prestador_num_documento foreign key (prestador_num_documento) references persona (num_documento);
alter table administrador add constraint administrador_num_documento foreign key (administrador_num_documento) references persona (num_documento);
alter table usuario_rol add constraint fk_id_usuario foreign key (fk_id_usuario) references usuario (id_usuario);
alter table usuario_rol add constraint fk_id_rol foreign key (fk_id_rol) references rol (id_rol);
alter table vehiculo_doc_legal add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table vehiculo_doc_legal add constraint fk_id_doc_legal foreign key (fk_id_doc_legal) references doc_legal (id_doc_legal);
alter table usuario add constraint fk_num_documento foreign key (fk_num_documento) references persona (num_documento);
alter table usuario add constraint fk_administrador_num_documento foreign key (fk_administrador_num_documento) references administrador (administrador_num_documento);
alter table pet_reserva add constraint fk_administrador_num_documento foreign key (fk_administrador_num_documento) references administrador (administrador_num_documento);
alter table pet_reserva add constraint fk_id_categoria foreign key (fk_id_categoria) references categoria (id_categoria);
alter table reserva add constraint fk_id_pet_reserva foreign key (fk_id_pet_reserva) references pet_reserva (id_pet_reserva);
alter table reserva add constraint fk_num_placa foreign key (fk_num_placa) references vehiculo (num_placa);
alter table acuerdo add constraint fk_prestador_num_documento foreign key (fk_prestador_num_documento) references prestador_vehiculo (prestador_num_documento);
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






----la base de datos se llama reserva



alter table alquiler. add constraint pk_ primary key();
alter table alquiler. add constraint pk_ primary key();
alter table alquiler. add constraint pk_ primary key();
alter table alquiler. add constraint pk_ primary key();
alter table alquiler. add constraint pk_ primary key();






































