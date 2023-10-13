--
-- PostgreSQL database dump
--

-- Dumped from database version 15.3
-- Dumped by pg_dump version 15.1

-- Started on 2023-10-13 17:27:22

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 234 (class 1255 OID 17170)
-- Name: actualizarautobusexistente(integer, character varying, boolean, timestamp without time zone, integer, integer, boolean); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.actualizarautobusexistente(IN autobus_id integer, IN placa character varying, IN enoperacion boolean, IN tiempoultimomantenimiento timestamp without time zone, IN horasenoperacion integer, IN cargadorid integer, IN enuso boolean)
    LANGUAGE sql
    AS $$
UPDATE Autobuses
SET placa = placa, enoperacion = enoperacion, tiempoultimomantenimiento = tiempoultimomantenimiento, horasenoperacion = horasenoperacion, cargadorid = cargadorid, enuso = enuso
WHERE id = autobus_id
RETURNING *;
$$;


ALTER PROCEDURE public.actualizarautobusexistente(IN autobus_id integer, IN placa character varying, IN enoperacion boolean, IN tiempoultimomantenimiento timestamp without time zone, IN horasenoperacion integer, IN cargadorid integer, IN enuso boolean) OWNER TO camilojaramillo;

--
-- TOC entry 229 (class 1255 OID 17165)
-- Name: actualizarcargadorexistente(integer, timestamp without time zone, timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.actualizarcargadorexistente(IN cargador_id integer, IN horainicio timestamp without time zone, IN horafin timestamp without time zone)
    LANGUAGE sql
    AS $$
UPDATE Cargadores
SET horainicio = horainicio, horafin = horafin
WHERE id = cargador_id
RETURNING *;
$$;


ALTER PROCEDURE public.actualizarcargadorexistente(IN cargador_id integer, IN horainicio timestamp without time zone, IN horafin timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 240 (class 1255 OID 17176)
-- Name: actualizaroperacionhoradia(integer, integer, timestamp without time zone, integer, integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.actualizaroperacionhoradia(IN hora_id integer, IN autobus_id integer, IN hora timestamp without time zone, IN busesenoperacion integer, IN cargadoresenuso integer)
    LANGUAGE sql
    AS $$
UPDATE Horarios
SET hora = hora, busesenoperacion = busesenoperacion, cargadoresenuso = cargadoresenuso
WHERE id = hora_id
RETURNING *;
$$;


ALTER PROCEDURE public.actualizaroperacionhoradia(IN hora_id integer, IN autobus_id integer, IN hora timestamp without time zone, IN busesenoperacion integer, IN cargadoresenuso integer) OWNER TO camilojaramillo;

--
-- TOC entry 224 (class 1255 OID 17160)
-- Name: actualizarutilizacionautobus(integer, timestamp without time zone, timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.actualizarutilizacionautobus(IN autobusid integer, IN usohoraanterior timestamp without time zone, IN usohoranueva timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Verificar si el autobús puede ser actualizado según las reglas del dominio
    IF NOT EXISTS (SELECT 1 FROM Autobuses WHERE Id = autobusId AND EnUso = false AND (EXTRACT(HOUR FROM usoHoraNueva) BETWEEN 5 AND 8 OR EXTRACT(HOUR FROM usoHoraNueva) BETWEEN 16 AND 19)) THEN
        RAISE EXCEPTION 'No se puede actualizar la utilización del autobús en horario pico.';
    END IF;

    -- Actualizar la utilización del autobús
    UPDATE UsosAutobus
    SET HoraInicio = usoHoraNueva,
        HoraFin = usoHoraNueva + interval '6 hours'
    WHERE AutobusId = autobusId
      AND HoraInicio = usoHoraAnterior;
END;
$$;


ALTER PROCEDURE public.actualizarutilizacionautobus(IN autobusid integer, IN usohoraanterior timestamp without time zone, IN usohoranueva timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 237 (class 1255 OID 17173)
-- Name: actualizarutilizacioncargadorhoradia(integer, integer, timestamp without time zone, integer, integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.actualizarutilizacioncargadorhoradia(IN hora_id integer, IN cargador_id integer, IN hora timestamp without time zone, IN busesenuso integer, IN cargadoresenuso integer)
    LANGUAGE sql
    AS $$
UPDATE Horarios
SET hora = hora, busesenoperacion = busesenuso, cargadoresenuso = cargadoresenuso
WHERE id = hora_id
RETURNING *;
$$;


ALTER PROCEDURE public.actualizarutilizacioncargadorhoradia(IN hora_id integer, IN cargador_id integer, IN hora timestamp without time zone, IN busesenuso integer, IN cargadoresenuso integer) OWNER TO camilojaramillo;

--
-- TOC entry 233 (class 1255 OID 17169)
-- Name: crearnuevoautobus(character varying, boolean, timestamp without time zone, integer, integer, boolean); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.crearnuevoautobus(IN placa character varying, IN enoperacion boolean, IN tiempoultimomantenimiento timestamp without time zone, IN horasenoperacion integer, IN cargadorid integer, IN enuso boolean)
    LANGUAGE sql
    AS $$
INSERT INTO Autobuses (placa, enoperacion, tiempoultimomantenimiento, horasenoperacion, cargadorid, enuso)
VALUES (placa, enoperacion, tiempoultimomantenimiento, horasenoperacion, cargadorid, enuso)
RETURNING *;
$$;


ALTER PROCEDURE public.crearnuevoautobus(IN placa character varying, IN enoperacion boolean, IN tiempoultimomantenimiento timestamp without time zone, IN horasenoperacion integer, IN cargadorid integer, IN enuso boolean) OWNER TO camilojaramillo;

--
-- TOC entry 228 (class 1255 OID 17164)
-- Name: crearnuevocargador(timestamp without time zone, timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.crearnuevocargador(IN horainicio timestamp without time zone, IN horafin timestamp without time zone)
    LANGUAGE sql
    AS $$
INSERT INTO Cargadores (horainicio, horafin, enuso, ciclosdecarga)
VALUES (horainicio, horafin, false, 0)
RETURNING *;
$$;


ALTER PROCEDURE public.crearnuevocargador(IN horainicio timestamp without time zone, IN horafin timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 235 (class 1255 OID 17171)
-- Name: eliminarautobusexistente(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.eliminarautobusexistente(IN autobus_id integer)
    LANGUAGE sql
    AS $$
DELETE FROM Autobuses WHERE id = autobus_id;
$$;


ALTER PROCEDURE public.eliminarautobusexistente(IN autobus_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 230 (class 1255 OID 17166)
-- Name: eliminarcargadorexistente(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.eliminarcargadorexistente(IN cargador_id integer)
    LANGUAGE sql
    AS $$
DELETE FROM Cargadores WHERE id = cargador_id;
$$;


ALTER PROCEDURE public.eliminarcargadorexistente(IN cargador_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 241 (class 1255 OID 17177)
-- Name: eliminaroperacionhoradia(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.eliminaroperacionhoradia(IN hora_id integer)
    LANGUAGE sql
    AS $$
DELETE FROM Horarios WHERE id = hora_id;
$$;


ALTER PROCEDURE public.eliminaroperacionhoradia(IN hora_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 225 (class 1255 OID 17161)
-- Name: eliminarutilizacionautobus(integer, timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.eliminarutilizacionautobus(IN autobusid integer, IN usohoraeliminar timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Verificar si el autobús puede eliminar su uso según las reglas del dominio
    IF NOT EXISTS (SELECT 1 FROM Autobuses WHERE Id = autobusId AND EnUso = false AND (EXTRACT(HOUR FROM usoHoraEliminar) BETWEEN 5 AND 8 OR EXTRACT(HOUR FROM usoHoraEliminar) BETWEEN 16 AND 19)) THEN
        RAISE EXCEPTION 'No se puede eliminar la utilización del autobús en horario pico.';
    END IF;

    -- Eliminar la utilización del autobús
    DELETE FROM UsosAutobus
    WHERE AutobusId = autobusId
      AND HoraInicio = usoHoraEliminar;
END;
$$;


ALTER PROCEDURE public.eliminarutilizacionautobus(IN autobusid integer, IN usohoraeliminar timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 238 (class 1255 OID 17174)
-- Name: eliminarutilizacioncargadorhoradia(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.eliminarutilizacioncargadorhoradia(IN hora_id integer)
    LANGUAGE sql
    AS $$
DELETE FROM Horarios WHERE id = hora_id;
$$;


ALTER PROCEDURE public.eliminarutilizacioncargadorhoradia(IN hora_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 231 (class 1255 OID 17167)
-- Name: obtenerautobusesregistrados(); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerautobusesregistrados()
    LANGUAGE sql
    AS $$
SELECT * FROM Autobuses;
$$;


ALTER PROCEDURE public.obtenerautobusesregistrados() OWNER TO camilojaramillo;

--
-- TOC entry 232 (class 1255 OID 17168)
-- Name: obtenerautobusporid(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerautobusporid(IN autobus_id integer)
    LANGUAGE sql
    AS $$
SELECT * FROM Autobuses WHERE id = autobus_id;
$$;


ALTER PROCEDURE public.obtenerautobusporid(IN autobus_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 226 (class 1255 OID 17162)
-- Name: obtenercargadoresregistrados(); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenercargadoresregistrados()
    LANGUAGE sql
    AS $$
SELECT * FROM Cargadores;
$$;


ALTER PROCEDURE public.obtenercargadoresregistrados() OWNER TO camilojaramillo;

--
-- TOC entry 227 (class 1255 OID 17163)
-- Name: obtenercargadorporid(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenercargadorporid(IN cargador_id integer)
    LANGUAGE sql
    AS $$
SELECT * FROM Cargadores WHERE id = cargador_id;
$$;


ALTER PROCEDURE public.obtenercargadorporid(IN cargador_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 246 (class 1255 OID 17178)
-- Name: obtenerhorasregistradas(); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerhorasregistradas()
    LANGUAGE sql
    AS $$
SELECT * FROM Horarios;
$$;


ALTER PROCEDURE public.obtenerhorasregistradas() OWNER TO camilojaramillo;

--
-- TOC entry 247 (class 1255 OID 17179)
-- Name: obtenerinformehoraporid(integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerinformehoraporid(IN hora_id integer)
    LANGUAGE sql
    AS $$
SELECT * FROM Horarios WHERE id = hora_id;
$$;


ALTER PROCEDURE public.obtenerinformehoraporid(IN hora_id integer) OWNER TO camilojaramillo;

--
-- TOC entry 255 (class 1255 OID 17181)
-- Name: obtenerinformeutilizacionbusesporhora(timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerinformeutilizacionbusesporhora(IN hora timestamp without time zone)
    LANGUAGE sql
    AS $$
SELECT * FROM Horarios WHERE hora = hora AND eshorariopico = true;
$$;


ALTER PROCEDURE public.obtenerinformeutilizacionbusesporhora(IN hora timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 254 (class 1255 OID 17180)
-- Name: obtenerinformeutilizacioncargadoresporhora(timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.obtenerinformeutilizacioncargadoresporhora(IN hora timestamp without time zone)
    LANGUAGE sql
    AS $$
SELECT * FROM Horarios WHERE hora = hora AND eshorariopico = true;
$$;


ALTER PROCEDURE public.obtenerinformeutilizacioncargadoresporhora(IN hora timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 239 (class 1255 OID 17175)
-- Name: registraroperacionhoradia(integer, timestamp without time zone, integer, integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.registraroperacionhoradia(IN autobus_id integer, IN hora timestamp without time zone, IN busesenoperacion integer, IN cargadoresenuso integer)
    LANGUAGE sql
    AS $$
INSERT INTO Horarios (hora, eshorariopico, busesenoperacion, cargadoresenuso)
VALUES (hora, true, busesenoperacion, cargadoresenuso)
RETURNING *;
$$;


ALTER PROCEDURE public.registraroperacionhoradia(IN autobus_id integer, IN hora timestamp without time zone, IN busesenoperacion integer, IN cargadoresenuso integer) OWNER TO camilojaramillo;

--
-- TOC entry 223 (class 1255 OID 17159)
-- Name: registrarutilizacionautobus(integer, timestamp without time zone); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public.registrarutilizacionautobus(IN autobusid integer, IN usohora timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
    -- Verificar si el autobús puede usarse en ese momento según las reglas del dominio
    IF NOT EXISTS (SELECT 1 FROM Autobuses WHERE Id = autobusId AND EnUso = false AND (EXTRACT(HOUR FROM usoHora) BETWEEN 5 AND 8 OR EXTRACT(HOUR FROM usoHora) BETWEEN 16 AND 19)) THEN
        RAISE EXCEPTION 'No se puede registrar la utilización del autobús en horario pico.';
    END IF;

    -- Registrar la utilización del autobús
    INSERT INTO UsosAutobus (HoraInicio, HoraFin, AutobusId)
    VALUES (usoHora, usoHora + interval '6 hours', autobusId);
END;
$$;


ALTER PROCEDURE public.registrarutilizacionautobus(IN autobusid integer, IN usohora timestamp without time zone) OWNER TO camilojaramillo;

--
-- TOC entry 236 (class 1255 OID 17172)
-- Name: registrarutilizacióncargadorhoradía(integer, timestamp without time zone, integer, integer); Type: PROCEDURE; Schema: public; Owner: camilojaramillo
--

CREATE PROCEDURE public."registrarutilizacióncargadorhoradía"(IN cargador_id integer, IN hora timestamp without time zone, IN busesenuso integer, IN cargadoresenuso integer)
    LANGUAGE sql
    AS $$
INSERT INTO Horarios (hora, eshorariopico, busesenoperacion, cargadoresenuso)
VALUES (hora, true, busesenuso, cargadoresenuso)
RETURNING *;
$$;


ALTER PROCEDURE public."registrarutilizacióncargadorhoradía"(IN cargador_id integer, IN hora timestamp without time zone, IN busesenuso integer, IN cargadoresenuso integer) OWNER TO camilojaramillo;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 222 (class 1259 OID 17154)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: camilojaramillo
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO camilojaramillo;

--
-- TOC entry 219 (class 1259 OID 17129)
-- Name: autobuses; Type: TABLE; Schema: public; Owner: camilojaramillo
--

CREATE TABLE public.autobuses (
    id integer NOT NULL,
    placa character varying NOT NULL,
    enoperacion boolean NOT NULL,
    tiempoultimomantenimiento timestamp without time zone NOT NULL,
    horasenoperacion integer NOT NULL,
    cargadorid integer,
    enuso boolean NOT NULL
);


ALTER TABLE public.autobuses OWNER TO camilojaramillo;

--
-- TOC entry 218 (class 1259 OID 17128)
-- Name: autobuses_id_seq; Type: SEQUENCE; Schema: public; Owner: camilojaramillo
--

CREATE SEQUENCE public.autobuses_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.autobuses_id_seq OWNER TO camilojaramillo;

--
-- TOC entry 4350 (class 0 OID 0)
-- Dependencies: 218
-- Name: autobuses_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: camilojaramillo
--

ALTER SEQUENCE public.autobuses_id_seq OWNED BY public.autobuses.id;


--
-- TOC entry 215 (class 1259 OID 17115)
-- Name: cargadores; Type: TABLE; Schema: public; Owner: camilojaramillo
--

CREATE TABLE public.cargadores (
    id integer NOT NULL,
    horainicio timestamp without time zone NOT NULL,
    horafin timestamp without time zone NOT NULL,
    enuso boolean NOT NULL,
    ciclosdecarga integer NOT NULL
);


ALTER TABLE public.cargadores OWNER TO camilojaramillo;

--
-- TOC entry 214 (class 1259 OID 17114)
-- Name: cargadores_id_seq; Type: SEQUENCE; Schema: public; Owner: camilojaramillo
--

CREATE SEQUENCE public.cargadores_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.cargadores_id_seq OWNER TO camilojaramillo;

--
-- TOC entry 4351 (class 0 OID 0)
-- Dependencies: 214
-- Name: cargadores_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: camilojaramillo
--

ALTER SEQUENCE public.cargadores_id_seq OWNED BY public.cargadores.id;


--
-- TOC entry 217 (class 1259 OID 17122)
-- Name: horarios; Type: TABLE; Schema: public; Owner: camilojaramillo
--

CREATE TABLE public.horarios (
    id integer NOT NULL,
    hora timestamp without time zone NOT NULL,
    eshorariopico boolean NOT NULL,
    busesenoperacion integer NOT NULL,
    cargadoresenuso integer NOT NULL
);


ALTER TABLE public.horarios OWNER TO camilojaramillo;

--
-- TOC entry 216 (class 1259 OID 17121)
-- Name: horarios_id_seq; Type: SEQUENCE; Schema: public; Owner: camilojaramillo
--

CREATE SEQUENCE public.horarios_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.horarios_id_seq OWNER TO camilojaramillo;

--
-- TOC entry 4352 (class 0 OID 0)
-- Dependencies: 216
-- Name: horarios_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: camilojaramillo
--

ALTER SEQUENCE public.horarios_id_seq OWNED BY public.horarios.id;


--
-- TOC entry 221 (class 1259 OID 17143)
-- Name: usosautobus; Type: TABLE; Schema: public; Owner: camilojaramillo
--

CREATE TABLE public.usosautobus (
    id integer NOT NULL,
    horainicio timestamp without time zone NOT NULL,
    horafin timestamp without time zone NOT NULL,
    autobusid integer NOT NULL
);


ALTER TABLE public.usosautobus OWNER TO camilojaramillo;

--
-- TOC entry 220 (class 1259 OID 17142)
-- Name: usosautobus_id_seq; Type: SEQUENCE; Schema: public; Owner: camilojaramillo
--

CREATE SEQUENCE public.usosautobus_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.usosautobus_id_seq OWNER TO camilojaramillo;

--
-- TOC entry 4353 (class 0 OID 0)
-- Dependencies: 220
-- Name: usosautobus_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: camilojaramillo
--

ALTER SEQUENCE public.usosautobus_id_seq OWNED BY public.usosautobus.id;


--
-- TOC entry 4180 (class 2604 OID 17132)
-- Name: autobuses id; Type: DEFAULT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.autobuses ALTER COLUMN id SET DEFAULT nextval('public.autobuses_id_seq'::regclass);


--
-- TOC entry 4178 (class 2604 OID 17118)
-- Name: cargadores id; Type: DEFAULT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.cargadores ALTER COLUMN id SET DEFAULT nextval('public.cargadores_id_seq'::regclass);


--
-- TOC entry 4179 (class 2604 OID 17125)
-- Name: horarios id; Type: DEFAULT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.horarios ALTER COLUMN id SET DEFAULT nextval('public.horarios_id_seq'::regclass);


--
-- TOC entry 4181 (class 2604 OID 17146)
-- Name: usosautobus id; Type: DEFAULT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.usosautobus ALTER COLUMN id SET DEFAULT nextval('public.usosautobus_id_seq'::regclass);


--
-- TOC entry 4344 (class 0 OID 17154)
-- Dependencies: 222
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: camilojaramillo
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20231012205842_CreateAutobusTable	7.0.12
20231012205848_CreateCargadorTable	7.0.12
20231012205853_CreateHorarioTable	7.0.12
20231012205859_CreateUsoAutobusTable	7.0.12
\.


--
-- TOC entry 4341 (class 0 OID 17129)
-- Dependencies: 219
-- Data for Name: autobuses; Type: TABLE DATA; Schema: public; Owner: camilojaramillo
--

COPY public.autobuses (id, placa, enoperacion, tiempoultimomantenimiento, horasenoperacion, cargadorid, enuso) FROM stdin;
1	UCBN78Y	t	2023-09-09 22:40:02.15177	1730	1	t
2	AQJGUDB	f	2023-10-05 18:40:02.15177	1042	2	t
3	82SSD4T	t	2023-09-12 18:40:02.15177	1929	3	t
4	9CX9IGQ	t	2023-09-10 04:40:02.15177	701	4	f
5	16B8M2P	f	2023-09-24 07:40:02.15177	1894	5	f
6	6ZLIFQO	t	2023-09-21 03:40:02.15177	697	6	t
7	YOOL2J5	f	2023-10-10 20:40:02.15177	1163	7	t
8	LR3Z43I	f	2023-09-26 17:40:02.15177	703	8	t
9	W3QZV7V	t	2023-09-14 02:40:02.15177	1595	9	f
10	MRC6PMZ	f	2023-10-04 23:40:02.15177	883	10	t
11	38HQ6C5	t	2023-09-18 14:40:02.15177	647	11	t
12	PP363PW	t	2023-09-13 20:40:02.15177	683	12	f
13	CL82MEC	t	2023-10-11 09:40:02.15177	1781	13	t
14	4M8K6EX	t	2023-09-25 12:40:02.15177	1930	14	f
15	I91WKD3	f	2023-10-03 11:40:02.15177	1675	15	t
16	SLL5F8Y	t	2023-09-20 15:40:02.15177	1712	16	f
17	ILBM87C	f	2023-09-14 21:40:02.15177	570	17	t
18	YP01TES	f	2023-09-09 15:40:02.15177	1362	18	t
19	0X285JS	t	2023-10-05 21:40:02.15177	486	19	t
20	7BBAU0J	t	2023-09-21 17:40:02.15177	827	20	f
\.


--
-- TOC entry 4337 (class 0 OID 17115)
-- Dependencies: 215
-- Data for Name: cargadores; Type: TABLE DATA; Schema: public; Owner: camilojaramillo
--

COPY public.cargadores (id, horainicio, horafin, enuso, ciclosdecarga) FROM stdin;
1	2023-10-12 16:00:00	2023-10-13 07:00:00	t	8
2	2023-10-12 16:00:00	2023-10-12 23:00:00	t	8
3	2023-10-12 16:00:00	2023-10-12 23:00:00	f	5
4	2023-10-12 16:00:00	2023-10-12 17:00:00	t	6
5	2023-10-12 16:00:00	2023-10-13 11:00:00	f	3
6	2023-10-12 16:00:00	2023-10-13 10:00:00	t	10
7	2023-10-12 16:00:00	2023-10-12 20:00:00	t	1
8	2023-10-12 16:00:00	2023-10-12 19:00:00	f	7
9	2023-10-12 16:00:00	2023-10-13 02:00:00	t	9
10	2023-10-12 16:00:00	2023-10-13 00:00:00	f	2
11	2023-10-12 16:00:00	2023-10-12 18:00:00	t	1
12	2023-10-12 16:00:00	2023-10-13 05:00:00	f	3
13	2023-10-12 16:00:00	2023-10-13 01:00:00	t	4
14	2023-10-12 16:00:00	2023-10-12 19:00:00	t	6
15	2023-10-12 16:00:00	2023-10-12 17:00:00	f	1
16	2023-10-12 16:00:00	2023-10-12 17:00:00	t	4
17	2023-10-12 16:00:00	2023-10-13 07:00:00	t	6
18	2023-10-12 16:00:00	2023-10-12 22:00:00	f	4
19	2023-10-12 16:00:00	2023-10-13 15:00:00	t	8
20	2023-10-12 16:00:00	2023-10-13 12:00:00	t	4
\.


--
-- TOC entry 4339 (class 0 OID 17122)
-- Dependencies: 217
-- Data for Name: horarios; Type: TABLE DATA; Schema: public; Owner: camilojaramillo
--

COPY public.horarios (id, hora, eshorariopico, busesenoperacion, cargadoresenuso) FROM stdin;
1	2023-10-13 07:00:00	f	5	20
2	2023-10-13 15:00:00	f	15	0
3	2023-10-13 05:00:00	f	8	2
4	2023-10-12 22:00:00	f	14	8
5	2023-10-13 09:00:00	f	4	12
6	2023-10-13 01:00:00	t	30	12
7	2023-10-13 10:00:00	f	5	2
8	2023-10-13 03:00:00	f	28	12
9	2023-10-12 20:00:00	f	0	6
10	2023-10-13 00:00:00	t	11	4
11	2023-10-12 22:00:00	t	13	13
12	2023-10-12 19:00:00	t	7	1
13	2023-10-13 01:00:00	f	7	12
14	2023-10-12 20:00:00	t	24	9
15	2023-10-12 17:00:00	t	22	5
16	2023-10-13 11:00:00	t	6	7
17	2023-10-13 04:00:00	f	30	4
18	2023-10-13 01:00:00	t	2	4
19	2023-10-12 22:00:00	t	26	12
20	2023-10-13 10:00:00	f	15	6
\.


--
-- TOC entry 4343 (class 0 OID 17143)
-- Dependencies: 221
-- Data for Name: usosautobus; Type: TABLE DATA; Schema: public; Owner: camilojaramillo
--

COPY public.usosautobus (id, horainicio, horafin, autobusid) FROM stdin;
21	2023-10-13 09:00:00	2023-10-13 18:00:00	4
22	2023-10-13 16:00:00	2023-10-13 18:00:00	4
23	2023-10-13 14:00:00	2023-10-13 18:00:00	2
24	2023-10-13 09:00:00	2023-10-13 10:00:00	1
25	2023-10-13 15:00:00	2023-10-13 18:00:00	15
26	2023-10-12 18:00:00	2023-10-13 03:00:00	19
27	2023-10-12 21:00:00	2023-10-13 02:00:00	7
28	2023-10-13 14:00:00	2023-10-13 18:00:00	12
29	2023-10-13 05:00:00	2023-10-13 14:00:00	19
30	2023-10-13 06:00:00	2023-10-13 09:00:00	16
31	2023-10-13 02:00:00	2023-10-13 06:00:00	4
32	2023-10-13 14:00:00	2023-10-13 17:00:00	16
33	2023-10-13 14:00:00	2023-10-14 00:00:00	10
34	2023-10-13 15:00:00	2023-10-13 23:00:00	6
35	2023-10-13 13:00:00	2023-10-13 14:00:00	20
36	2023-10-13 13:00:00	2023-10-13 20:00:00	10
37	2023-10-13 00:00:00	2023-10-13 10:00:00	8
38	2023-10-13 06:00:00	2023-10-13 14:00:00	16
39	2023-10-13 03:00:00	2023-10-13 08:00:00	1
40	2023-10-12 18:00:00	2023-10-13 03:00:00	19
\.


--
-- TOC entry 4354 (class 0 OID 0)
-- Dependencies: 218
-- Name: autobuses_id_seq; Type: SEQUENCE SET; Schema: public; Owner: camilojaramillo
--

SELECT pg_catalog.setval('public.autobuses_id_seq', 20, true);


--
-- TOC entry 4355 (class 0 OID 0)
-- Dependencies: 214
-- Name: cargadores_id_seq; Type: SEQUENCE SET; Schema: public; Owner: camilojaramillo
--

SELECT pg_catalog.setval('public.cargadores_id_seq', 20, true);


--
-- TOC entry 4356 (class 0 OID 0)
-- Dependencies: 216
-- Name: horarios_id_seq; Type: SEQUENCE SET; Schema: public; Owner: camilojaramillo
--

SELECT pg_catalog.setval('public.horarios_id_seq', 20, true);


--
-- TOC entry 4357 (class 0 OID 0)
-- Dependencies: 220
-- Name: usosautobus_id_seq; Type: SEQUENCE SET; Schema: public; Owner: camilojaramillo
--

SELECT pg_catalog.setval('public.usosautobus_id_seq', 40, true);


--
-- TOC entry 4191 (class 2606 OID 17158)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4187 (class 2606 OID 17136)
-- Name: autobuses autobuses_pkey; Type: CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.autobuses
    ADD CONSTRAINT autobuses_pkey PRIMARY KEY (id);


--
-- TOC entry 4183 (class 2606 OID 17120)
-- Name: cargadores cargadores_pkey; Type: CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.cargadores
    ADD CONSTRAINT cargadores_pkey PRIMARY KEY (id);


--
-- TOC entry 4185 (class 2606 OID 17127)
-- Name: horarios horarios_pkey; Type: CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.horarios
    ADD CONSTRAINT horarios_pkey PRIMARY KEY (id);


--
-- TOC entry 4189 (class 2606 OID 17148)
-- Name: usosautobus usosautobus_pkey; Type: CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.usosautobus
    ADD CONSTRAINT usosautobus_pkey PRIMARY KEY (id);


--
-- TOC entry 4192 (class 2606 OID 17137)
-- Name: autobuses autobuses_cargadorid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.autobuses
    ADD CONSTRAINT autobuses_cargadorid_fkey FOREIGN KEY (cargadorid) REFERENCES public.cargadores(id);


--
-- TOC entry 4193 (class 2606 OID 17149)
-- Name: usosautobus usosautobus_autobusid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: camilojaramillo
--

ALTER TABLE ONLY public.usosautobus
    ADD CONSTRAINT usosautobus_autobusid_fkey FOREIGN KEY (autobusid) REFERENCES public.autobuses(id);


-- Completed on 2023-10-13 17:27:33

--
-- PostgreSQL database dump complete
--

