-- Table: public.worktime

-- DROP TABLE public.worktime;

CREATE TABLE public.worktime
(
    id bigint NOT NULL DEFAULT nextval('worktime_id_seq'::regclass),
    weekday character varying(3) COLLATE pg_catalog."default" NOT NULL,
    start_time time without time zone NOT NULL,
    finish_time time without time zone NOT NULL,
    CONSTRAINT worktime_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.worktime
    OWNER to postgres;