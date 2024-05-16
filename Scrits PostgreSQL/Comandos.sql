CREATE OR REPLACE PROCEDURE insertar_datos_pet(
	IN fecha_ini date,
	IN fecha_fin date,
	IN hora_ini character varying,
	IN hora_fin character varying,
	IN fk_id_categoria integer,
	IN fk_id_usuario integer,
	IN fk_id_ubicacion_ini integer,
	IN fk_id_ubicacion_fin integer)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO pet_reserva (fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_categoria, fk_id_usuario, fk_id_ubicacion_ini, fk_id_ubicacion_fin)
    VALUES (fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_categoria, fk_id_usuario, fk_id_ubicacion_ini, fk_id_ubicacion_fin);
    
    COMMIT;
    
  END;
$$;


CREATE OR REPLACE PROCEDURE insertar_reserva(
    fk_id_pet_reserva INT,
    fk_num_placa VARCHAR(20)
)
LANGUAGE plpgsql
AS $$
BEGIN
    insert into reserva(acep_fecha,fk_id_pet_reserva, fk_num_placa)
    VALUES (current_timestamp, fk_id_pet_reserva, fk_num_placa);
    
    COMMIT;
    
  END;
$$;


CREATE FUNCTION sp_estado_pet_reserva(id_pet_reserva_in INT) RETURNS VOID AS $$
BEGIN
  UPDATE pet_reserva SET estado = 'acpt' WHERE id_pet_reserva = id_pet_reserva_in;
END;
$$ LANGUAGE plpgsql;

--SELECT sp_estado_pet_reserva(123); 

insert into "Group" (name,createddate) 
VALUES ('Test', current_timestamp);
A mostrar ese valor en un formato diferente cambie la configuración de su cliente SQL o formatee el valor al seleccionar los datos:

select name, to_char(createddate, ''yyyymmdd hh:mi:ss tt') as created_date
from "Group";'

--- disponibilidad de vehiculo --no se usas
CREATE OR REPLACE FUNCTION disponible_vehi(fecha_inicio_param DATE, fecha_fin_param DATE) RETURNS SETOF VARCHAR(10) AS $$
DECLARE
    placa_ini_list VARCHAR(10)[];
    placa_fin_list VARCHAR(10)[];
BEGIN
    -- Encuentra las placas de los vehículos reservados cuyas fechas de inicio están después de la fecha de inicio proporcionada
    SELECT array_agg(r.fk_num_placa) INTO placa_ini_list
    FROM reserva r
    INNER JOIN pet_reserva pr ON pr.id_pet_reserva = r.fk_id_pet_reserva
    WHERE pr.fecha_ini >= fecha_inicio_param;

    -- Encuentra las placas de los vehículos reservados cuyas fechas de finalización están antes de la fecha de finalización proporcionada
    SELECT array_agg(r.fk_num_placa) INTO placa_fin_list
    FROM reserva r
    INNER JOIN pet_reserva pr ON pr.id_pet_reserva = r.fk_id_pet_reserva
    WHERE pr.fecha_fin <= fecha_fin_param;

    -- Devuelve las placas coincidentes
    RETURN QUERY SELECT UNNEST(placa_ini_list) INTERSECT SELECT UNNEST(placa_fin_list);
END;
$$ LANGUAGE plpgsql;





CREATE OR REPLACE PROCEDURE insertar_espce_vehiculo(
    IN p_modelo Integer,
    IN p_color varchar(45),
    IN p_num_chasis decimal(10,10),
    IN p_modelo_motor Integer,
    IN p_cilindraje Integer,
    IN p_fk_id_tipo_combustible Integer
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO public.espec_vehiculo(
        modelo, color, num_chasis, modelo_motor, cilindraje, fk_id_tipo_combustible
    )
    VALUES (
        p_modelo, p_color, p_num_chasis, p_modelo_motor, p_cilindraje, p_fk_id_tipo_combustible
    );
END;
$$;


CREATE OR REPLACE PROCEDURE insertar_vehiculo(
    IN p_num_placa varchar(20),
    IN p_capacidad_pasajeros INTEGER,
    IN p_capacidad_carga varchar(25),
    IN p_fk_id_categoria INTEGER,
    IN p_fk_id_tipo_direccion INTEGER,
    IN p_fk_id_caja_cambios INTEGER,
    IN p_fk_id_marca INTEGER,
    IN p_fk_id_espec_vehiculo INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO public.vehiculo(
        num_placa, capacidad_pasajeros, capacidad_carga, fk_id_categoria, fk_id_tipo_direccion, 
        fk_id_caja_cambios, fk_id_marca, fk_id_espec_vehiculo
    )
    VALUES (
        p_num_placa, p_capacidad_pasajeros, p_capacidad_carga, p_fk_id_categoria, p_fk_id_tipo_direccion, 
        p_fk_id_caja_cambios,p_fk_id_marca, p_fk_id_espec_vehiculo
    );
END;
$$;










--Rol Administrador...

--CREATE ROLE Administrador;
--GRANT CONNECT ON DATABASE arquiler TO Administrador;
--GRANT USAGE ON SCHEMA public TO Administrador;
--GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO Administrador;
--ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO Administrador;
--GRANT USAGE ON ALL SEQUENCES IN SCHEMA public TO Administrador;
--ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT USAGE ON SEQUENCES TO Administrador;
--ALTER ROLE Administrador CREATEROLE;

-- Usuario Administrador...

--CREATE USER arango WITH PASSWORD 'root';
--GRANT Administrador TO arango;

--Rol Cliente...

--CREATE ROLE Cliente;
--GRANT CONNECT ON DATABASE arquiler TO Cliente;
--GRANT USAGE ON SCHEMA public TO Cliente;
--GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO Cliente;
--ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO Cliente;
--REVOKE INSERT, UPDATE, DELETE ON adminstrador FROM Cliente;
--REVOKE INSERT, UPDATE, DELETE ON vehiculo FROM Cliente;
--REVOKE INSERT, UPDATE, DELETE ON categoria FROM Cliente;
--GRANT USAGE ON ALL SEQUENCES IN SCHEMA public TO Cliente;

-- Usuario Cliente...

--CREATE USER cli_default WITH PASSWORD 'default';
--GRANT cli_default TO Cliente;


--Rol Prestador...

--CREATE ROLE Prestador;
--GRANT SELECT, INSERT, UPDATE, DELETE ON vehiculo TO Prestador;
--GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO Prestador;
--ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO Prestador;

-- Usuario Prestador...

--CREATE USER pres_default WITH PASSWORD 'default';
--GRANT Prestador TO pres_default;
--GRANT Cliente TO pres_default;

--- Consulta para ver todos los Usuarios
--SELECT usename FROM pg_user;
--- Consulta para ver todos Roles
/*
SELECT
      r.rolname,
      ARRAY(SELECT b.rolname
            FROM pg_catalog.pg_auth_members m
            JOIN pg_catalog.pg_roles b ON (m.roleid = b.oid)
            WHERE m.member = r.oid) as memberof
FROM pg_catalog.pg_roles r
WHERE r.rolname NOT IN ('pg_signal_backend','rds_iam',
                        'rds_replication','rds_superuser',
                        'rdsadmin','rdsrepladmin')
ORDER BY 1;
*/