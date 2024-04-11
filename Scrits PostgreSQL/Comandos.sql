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