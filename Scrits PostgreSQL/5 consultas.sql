SELECT * FROM pet_reserva where estado = 'wait' ORDER BY id_pet_reserva ASC;

select 
	ubicacion_ini.nombre_barrio as recojida,
    ubicacion_fin.nombre_barrio as devolucion,
    pr.fecha_ini,
    pr.fecha_fin,
    pr.hora_ini,
    pr.hora_fin,
    r.acep_fecha,
    r.fk_num_placa 
    from usuario u inner join pet_reserva pr on u.id_usuario = pr.fk_id_usuario
    inner join ubicacion ubicacion_ini ON ubicacion_ini.id_ubicacion = pr.fk_id_ubicacion_ini
    inner join ubicacion ubicacion_fin ON ubicacion_fin.id_ubicacion = pr.fk_id_ubicacion_fin 
    inner join reserva r ON r.fk_id_pet_reserva = pr.id_pet_reserva 
    where u.usuario = 'manuel' and r.estado_reserva = 'activa';

SELECT * from usuario  WHERE usuario = 'manuel' and contrasenia = 'root';
------- parte 1
SELECT *
FROM (
    SELECT *
    FROM vehiculo v
    WHERE v.num_placa NOT IN (
        SELECT r.fk_num_placa
        FROM reserva r
        INNER JOIN pet_reserva pr ON pr.id_pet_reserva = r.fk_id_pet_reserva
        WHERE r.estado_reserva = 'activa' AND (
            (pr.fecha_ini >= '2024-07-03' AND pr.fecha_fin <= '2024-08-06') OR
            (pr.fecha_ini <= '2024-07-03' AND pr.fecha_fin >= '2024-08-06') OR
            (pr.fecha_ini BETWEEN '2024-07-03' AND '2024-08-06') OR
            (pr.fecha_fin BETWEEN '2024-07-03' AND '2024-08-06')
        )
    )
) AS subconsulta
WHERE subconsulta.fk_id_categoria = 2;

 select * from vehiculo v where v.num_placa = '';"
 
 
SELECT
    pr.id_pet_reserva,
    per.primer_apellido || ' ' || per.segundo_apellido AS nombre_completo,
    pr.fecha_ini,
    pr.fecha_fin,
    pr.hora_ini,
    pr.hora_fin,
    ubi_ini.nombre_barrio AS Recojida,
    ubi_fin.nombre_barrio AS Devolucion,
    cate.tipo_vehiculo
FROM
    pet_reserva pr
INNER JOIN
    ubicacion ubi_ini ON ubi_ini.id_ubicacion = pr.fk_id_ubicacion_ini
INNER JOIN
    ubicacion ubi_fin ON ubi_fin.id_ubicacion = pr.fk_id_ubicacion_fin
INNER JOIN
    categoria cate ON cate.id_categoria = pr.fk_id_categoria
INNER JOIN
    usuario u ON u.id_usuario = pr.fk_id_usuario
INNER JOIN
    persona per ON per.num_documento = u.fk_num_documento
WHERE
    u.usuario = 'manuel'
ORDER BY
    pr.id_pet_reserva DESC
LIMIT 1;

