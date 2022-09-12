--
-- PostgreSQL database dump
--

-- Dumped from database version 14.5
-- Dumped by pg_dump version 14.5

-- Started on 2022-09-12 13:33:21

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 16433)
-- Name: Balance; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Balance" (
    "AccountId" integer,
    "InBalance" double precision,
    "Calculation" double precision,
    "RecId" integer NOT NULL,
    "Period" date
);


ALTER TABLE public."Balance" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 16450)
-- Name: Balance_RecId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Balance" ALTER COLUMN "RecId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Balance_RecId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 209 (class 1259 OID 16428)
-- Name: Payment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Payment" (
    "AccountId" integer NOT NULL,
    "Date" timestamp without time zone NOT NULL,
    "Sum" double precision,
    "PaymentGuid" text NOT NULL,
    "RecId" integer NOT NULL
);


ALTER TABLE public."Payment" OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16466)
-- Name: Payment_RecId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Payment" ALTER COLUMN "RecId" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Payment_RecId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 214 (class 1259 OID 16525)
-- Name: TurnoverBalance; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."TurnoverBalance" (
    "Period" date NOT NULL,
    "StartingBalance" double precision,
    "Accrued" double precision,
    "Paid" double precision,
    "EndingBalance" double precision
);


ALTER TABLE public."TurnoverBalance" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16436)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3178 (class 2606 OID 16440)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3180 (class 2606 OID 16529)
-- Name: TurnoverBalance TurnoverBalance_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."TurnoverBalance"
    ADD CONSTRAINT "TurnoverBalance_pkey" PRIMARY KEY ("Period");


-- Completed on 2022-09-12 13:33:22

--
-- PostgreSQL database dump complete
--

