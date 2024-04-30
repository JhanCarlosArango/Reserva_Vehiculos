CREATE OR REPLACE PROCEDURE insertar_datos_pet(
    fecha_ini DATE,
    fecha_fin DATE,
    hora_ini VARCHAR(20),
    hora_fin VARCHAR(20),
    fk_id_categoria INT,
    fk_id_usuario INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO pet_reserva (fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_categoria, fk_id_usuario)
    VALUES (fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_categoria, fk_id_usuario);
    
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
from "Group"














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