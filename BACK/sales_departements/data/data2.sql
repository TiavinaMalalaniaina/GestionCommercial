CREATE SEQUENCE "public".departement_seq START WITH 1;

CREATE SEQUENCE "public".employee_seq START WITH 1;

CREATE SEQUENCE "public".person_seq START WITH 1;

CREATE SEQUENCE "public".product_seq START WITH 1;

CREATE SEQUENCE "public".proforma_details_seq START WITH 1;

CREATE SEQUENCE "public".proforma_seq START WITH 1;

CREATE SEQUENCE "public".purchase_order_details_seq START WITH 1;

CREATE SEQUENCE "public".purchase_order_seq START WITH 1;

CREATE SEQUENCE "public".request_details_seq START WITH 1;

CREATE SEQUENCE "public".request_seq START WITH 1;

CREATE SEQUENCE "public".supplier_product_seq START WITH 1;

CREATE SEQUENCE "public".supplier_seq START WITH 1;


CREATE OR REPLACE FUNCTION public.custom_seq(in_prefix character varying, in_sequence_name character varying, in_digit_count integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
DECLARE
    seq_value INT;
    result VARCHAR;
BEGIN
    EXECUTE 'SELECT nextval(''' || in_sequence_name || '''::regclass)' INTO seq_value;
    result := in_prefix || LPAD(seq_value::TEXT, in_digit_count, '0');
    RETURN result;
END;
$function$
;


CREATE  TABLE "public".person ( 
	person_id            varchar DEFAULT custom_seq('PER'::character varying, 'person_seq'::character varying, 5) NOT NULL ,
	first_name           varchar(50)   ,
	last_name            varchar(50)   ,
	date_of_birth        date   ,
	gender               varchar(10)   ,
	phone_number         varchar(20)   ,
	address              varchar(255)   ,
	CONSTRAINT person_pkey PRIMARY KEY ( person_id )
 );

CREATE  TABLE "public".product ( 
	product_id           varchar DEFAULT custom_seq('PRO'::character varying, 'product_seq'::character varying, 5) NOT NULL ,
	product_name         varchar(100)   ,
	CONSTRAINT product_pkey PRIMARY KEY ( product_id ),
	CONSTRAINT product_product_name_key UNIQUE ( product_name ) 
 );

CREATE  TABLE "public".supplier ( 
	supplier_id          varchar DEFAULT custom_seq('SUP'::character varying, 'supplier_seq'::character varying, 5) NOT NULL ,
	name                 varchar(100)   ,
	contact_email        varchar(100)   ,
	contact_phone        varchar(20)   ,
	address              varchar(255)   ,
	CONSTRAINT supplier_pkey PRIMARY KEY ( supplier_id ),
	CONSTRAINT supplier_name_key UNIQUE ( name ) 
 );

CREATE  TABLE "public".supplier_product ( 
	supplier_product_id  varchar DEFAULT custom_seq('SPR'::character varying, 'supplier_product_seq'::character varying, 5) NOT NULL ,
	supplier_id          varchar   ,
	product_id           varchar   ,
	CONSTRAINT supplier_product_pkey PRIMARY KEY ( supplier_product_id ),
	CONSTRAINT supplier_product_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT supplier_product_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )   
 );

CREATE  TABLE "public".department ( 
	department_id        varchar DEFAULT custom_seq('DEP'::character varying, 'departement_seq'::character varying, 5) NOT NULL ,
	department_name      varchar(100)   ,
	department_head_id   varchar   ,
	CONSTRAINT department_pkey PRIMARY KEY ( department_id ),
	CONSTRAINT department_department_name_key UNIQUE ( department_name ) ,
	CONSTRAINT department_department_head_id_fkey FOREIGN KEY ( department_head_id ) REFERENCES "public".person( person_id )   
 );

CREATE  TABLE "public".employee ( 
	employee_id          varchar DEFAULT custom_seq('EMP'::character varying, 'employee_seq'::character varying, 5) NOT NULL ,
	person_id            varchar   ,
	department_id        varchar   ,
	hire_date            date   ,
	job_title            varchar(100)   ,
	salary               numeric(10,2)   ,
	email                varchar(100)   ,
	"password"           varchar(20)   ,
	daf                  boolean DEFAULT false  ,
	CONSTRAINT employee_pkey PRIMARY KEY ( employee_id ),
	CONSTRAINT employee_person_id_key UNIQUE ( person_id ) ,
	CONSTRAINT employee_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )   ,
	CONSTRAINT employee_person_id_fkey FOREIGN KEY ( person_id ) REFERENCES "public".person( person_id )   
 );

CREATE  TABLE "public".proforma ( 
	proforma_id          varchar DEFAULT custom_seq('PRO'::character varying, 'proforma_seq'::character varying, 5) NOT NULL ,
	issue_date           date   ,
	due_date             date   ,
	supplier_id          varchar   ,
	CONSTRAINT proforma_pkey PRIMARY KEY ( proforma_id ),
	CONSTRAINT proforma_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )   
 );

CREATE  TABLE "public".proforma_details ( 
	proforma_details_id  varchar DEFAULT custom_seq('PRD'::character varying, 'proforma_details_seq'::character varying, 5) NOT NULL ,
	proforma_id          varchar   ,
	product_id           varchar   ,
	quantity             integer   ,
	price                numeric(10,2)   ,
	CONSTRAINT proforma_details_pkey PRIMARY KEY ( proforma_details_id ),
	CONSTRAINT proforma_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT proforma_details_proforma_id_fkey FOREIGN KEY ( proforma_id ) REFERENCES "public".proforma( proforma_id )   
 );

CREATE  TABLE "public".purchase_order ( 
	purchase_order_id    varchar DEFAULT custom_seq('PUR'::character varying, 'purchase_order_seq'::character varying, 5) NOT NULL ,
	created_at           timestamp(0) DEFAULT CURRENT_TIMESTAMP  ,
	delivery_days        integer   ,
	supplier_id          varchar   ,
	validation           integer DEFAULT 10  ,
	CONSTRAINT purchase_order_pkey PRIMARY KEY ( purchase_order_id ),
	CONSTRAINT purchase_order_supplier_id_fkey FOREIGN KEY ( supplier_id ) REFERENCES "public".supplier( supplier_id )   
 );

CREATE  TABLE "public".purchase_order_details ( 
	purchase_order_details_id varchar DEFAULT custom_seq('PUD'::character varying, 'purchase_order_details_seq'::character varying, 5) NOT NULL ,
	purchase_order_id    varchar   ,
	product_id           varchar   ,
	quantity             float8   ,
	price                numeric(10,2)   ,
	CONSTRAINT purchase_order_details_pkey PRIMARY KEY ( purchase_order_details_id ),
	CONSTRAINT purchase_order_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT purchase_order_details_purchase_order_id_fkey FOREIGN KEY ( purchase_order_id ) REFERENCES "public".purchase_order( purchase_order_id )   
 );

CREATE  TABLE "public".request ( 
	request_id           varchar DEFAULT custom_seq('REQ'::character varying, 'request_seq'::character varying, 5) NOT NULL ,
	department_id        varchar   ,
	created_at           date   ,
	is_validated         boolean DEFAULT false  ,
	CONSTRAINT request_pkey PRIMARY KEY ( request_id ),
	CONSTRAINT request_department_id_fkey FOREIGN KEY ( department_id ) REFERENCES "public".department( department_id )   
 );

CREATE  TABLE "public".request_details ( 
	request_details_id   varchar DEFAULT custom_seq('RED'::character varying, 'request_details_seq'::character varying, 5) NOT NULL ,
	request_id           varchar   ,
	product_id           varchar   ,
	quantity             integer   ,
	reason               varchar(200)   ,
	is_validated         boolean DEFAULT false  ,
	CONSTRAINT request_details_pkey PRIMARY KEY ( request_details_id ),
	CONSTRAINT request_details_product_id_fkey FOREIGN KEY ( product_id ) REFERENCES "public".product( product_id )   ,
	CONSTRAINT request_details_request_id_fkey FOREIGN KEY ( request_id ) REFERENCES "public".request( request_id )   
 );


INSERT INTO "public".person( person_id, first_name, last_name, date_of_birth, gender, phone_number, address ) VALUES ( 'PER00001', 'RATIATIANA', 'Jean Mirlin', '2023-10-20', 'M', '0348262182', 'Ambohijanaka');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00001', 'ordinateur');
INSERT INTO "public".product( product_id, product_name ) VALUES ( 'PRO00002', 'projecteur');
INSERT INTO "public".department( department_id, department_name, department_head_id ) VALUES ( 'DEP00001', 'Informatique', 'PER00001');
INSERT INTO "public".employee( employee_id, person_id, department_id, hire_date, job_title, salary, email, password, daf ) VALUES ( 'EMP00001', 'PER00001', 'DEP00001', '2022-12-12', 'DEV', 2000000, 'jeanmirlin.r@gmail.com', 'mirlin', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00002', 'DEP00001', '2023-11-18', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00003', 'DEP00001', '2023-11-18', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00004', 'DEP00001', '2023-11-18', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00005', 'DEP00001', '2023-11-18', false);
INSERT INTO "public".request( request_id, department_id, created_at, is_validated ) VALUES ( 'REQ00001', 'DEP00001', '2020-11-11', true);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated ) VALUES ( 'RED00001', 'REQ00001', 'PRO00001', 5, 'informatisation', true);
INSERT INTO "public".request_details( request_details_id, request_id, product_id, quantity, reason, is_validated ) VALUES ( 'RED00002', 'REQ00001', 'PRO00001', 4, 'huhu', true);
