-- Table: public.driver_worktime

-- DROP TABLE public.driver_worktime;

CREATE TABLE public.driver_worktime
(
    id bigint NOT NULL DEFAULT nextval('driver_worktime_id_seq'::regclass),
    driver_id bigint NOT NULL,
    worktime_id bigint NOT NULL,
    CONSTRAINT driver_worktime_pkey PRIMARY KEY (id),
    CONSTRAINT driver_worktime_driver_id_fkey FOREIGN KEY (driver_id)
        REFERENCES public.driver (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT driver_worktime_worktime_id_fkey FOREIGN KEY (worktime_id)
        REFERENCES public.worktime (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.driver_worktime
    OWNER to postgres;