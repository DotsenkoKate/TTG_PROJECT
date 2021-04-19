-- Table: public.auto

-- DROP TABLE public.auto;

CREATE TABLE public.auto
(
    "number" character varying(12) COLLATE pg_catalog."default" NOT NULL,
    brand character varying(100) COLLATE pg_catalog."default" NOT NULL,
    model character varying(100) COLLATE pg_catalog."default",
    status character varying(16) COLLATE pg_catalog."default" NOT NULL,
    capacity integer NOT NULL,
    release_date date NOT NULL,
    driver_id bigint,
    way_number bigint NOT NULL,
    id bigint NOT NULL DEFAULT nextval('auto_id_seq'::regclass),
    CONSTRAINT auto_pkey PRIMARY KEY (id),
    CONSTRAINT driver_id_unique UNIQUE (driver_id),
    CONSTRAINT auto_driver_id_fkey FOREIGN KEY (driver_id)
        REFERENCES public.driver (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT auto_way_number_fkey FOREIGN KEY (way_number)
        REFERENCES public.way ("number") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.auto
    OWNER to postgres;