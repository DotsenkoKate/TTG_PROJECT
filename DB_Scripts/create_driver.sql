-- Table: public.driver

-- DROP TABLE public.driver;

CREATE TABLE public.driver
(
    id bigint NOT NULL DEFAULT nextval('driver_id_seq'::regclass),
    name character varying(200) COLLATE pg_catalog."default" NOT NULL,
    birthday date NOT NULL,
    passport character varying(11) COLLATE pg_catalog."default" NOT NULL,
    place character varying(200) COLLATE pg_catalog."default" NOT NULL,
    phonenumber character varying(11) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT driver_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.driver
    OWNER to postgres;