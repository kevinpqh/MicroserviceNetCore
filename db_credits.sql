/*CREACION DE BASE DE DATOS*/
-- db_credits

/*ESQUEMA*/
CREATE SCHEMA esc_credits AUTHORIZATION postgres;

/*SECUENCIA*/
CREATE SEQUENCE esc_credits."seq_creditdetailpay"
   INCREMENT BY 1
   MINVALUE 1
   MAXVALUE 99999;
   
ALTER SEQUENCE esc_credits."seq_creditdetailpay" OWNER TO postgres;

/*TABLAS*/
CREATE TABLE esc_credits."customer"
(
	IdCustomer INTEGER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
  	Name VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
  	Document VARCHAR(50) NOT NULL,
  	Photo VARCHAR(1500) NOT NULL,
  	IsActive BOOLEAN NOT NULL
);



CREATE TABLE esc_credits."typecredit"
(
	IdTypeCredit SERIAL PRIMARY KEY,
  	InternalCode VARCHAR(50) NOT NULL,  
  	Description VARCHAR(500) NOT NULL,
  	IsActive BOOLEAN NOT NULL	
);



CREATE TABLE esc_credits."credit"
(
	IdCredit INTEGER GENERATED BY DEFAULT AS IDENTITY PRIMARY KEY,
  	AmountTotal DECIMAL,
	PayTotal DECIMAL,
  	ResidueTotal DECIMAL,	
	Status VARCHAR(50) NOT NULL,
	CreationDate DATE,
	IdTypeCredit INTEGER,
	IdCustomer INTEGER,
  	IsActive BOOLEAN NOT NULL
);

ALTER TABLE IF EXISTS esc_credits."credit" ADD CONSTRAINT FK_Credit_IdCustomer 
FOREIGN KEY(IdCustomer) REFERENCES esc_credits."customer"(IdCustomer);

ALTER TABLE IF EXISTS esc_credits."credit" ADD CONSTRAINT FK_Credit_IdTypeCredit
FOREIGN KEY(IdTypeCredit) REFERENCES esc_credits."typecredit"(IdTypeCredit);


CREATE TABLE esc_credits."creditdetailpay"
(
	IdCreditDetailPay INTEGER PRIMARY KEY NOT NULL DEFAULT NEXTVAL('esc_credits."seq_creditdetailpay"'),
  	AmountPay DECIMAL,
  	Factor INT,
	CreationDate DATE,
  	IdCredit INT
)

ALTER TABLE IF EXISTS esc_credits."creditdetailpay" ADD CONSTRAINT FK_creditdetailpay_IdCredit 
FOREIGN KEY(IdCredit) REFERENCES esc_credits."credit"(IdCredit);



/*DATOS*/

INSERT INTO esc_credits."customer"(Name,LastName,Document,Photo,IsActive) 
VALUES('Jorge','Barrera Bustios','44852365','',True);
INSERT INTO esc_credits."customer"(Name,LastName,Document,Photo,IsActive) 
VALUES('Alan','Cespedes Valdivia','45851363','',True);
INSERT INTO esc_credits."customer"(Name,LastName,Document,Photo,IsActive) 
VALUES('Freddy','Figueroa Mori','33852311','',True);
INSERT INTO esc_credits."customer"(Name,LastName,Document,Photo,IsActive) 
VALUES('Danna','Cossi Cerrañez','23842326','',True);

--SELECT * FROM esc_credits."customer";

INSERT INTO esc_credits."typecredit"(InternalCode,Description,IsActive) 
VALUES('MY01','CREDITO PERSONAL',True);
INSERT INTO esc_credits."typecredit"(InternalCode,Description,IsActive) 
VALUES('DG02','CREDITO HIPOTECARIO',True);
INSERT INTO esc_credits."typecredit"(InternalCode,Description,IsActive) 
VALUES('MY02','CREDITO MYPE',False);
INSERT INTO esc_credits."typecredit"(InternalCode,Description,IsActive) 
VALUES('TP01','CREDITO UNIVERSITARIO',True);
INSERT INTO esc_credits."typecredit"(InternalCode,Description,IsActive) 
VALUES('MY03','CREDITO VEHICULAR',True);

--SELECT * FROM esc_credits."typecredit";


INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(1500,0,1500,'Abierto',current_date,1,1,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(1000,0,1000,'Abierto',current_date,1,1,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(500,0,500,'Abierto',current_date,1,2,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(2500,0,2500,'Abierto',current_date,4,3,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(100,0,100,'Abierto',current_date,3,3,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(5000,0,5000,'Abierto',current_date,5,4,True);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(1000,1000,0,'Pagado',current_date,5,4,False);

INSERT INTO esc_credits."credit"(AmountTotal,PayTotal,ResidueTotal,Status,CreationDate,IdTypeCredit,IdCustomer,IsActive) 
VALUES(1000,100,900,'Abierto',current_date,2,4,True);

--SELECT * FROM esc_credits."credit"

INSERT INTO esc_credits."creditdetailpay"(AmountPay,Factor,CreationDate,IdCredit) 
VALUES(50,-1,current_date,7);
INSERT INTO esc_credits."creditdetailpay"(AmountPay,Factor,CreationDate,IdCredit) 
VALUES(50,-1,current_date,7);

SELECT * FROM esc_credits."creditdetailpay";

/*FUNCIONES*/

CREATE OR REPLACE FUNCTION esc_credits.fn_list_credits()
RETURNS 
    TABLE(IdCredit integer, 
		  AmountTotal decimal,
		  PayTotal decimal,
		  ResidueTotal decimal,
		  Status character varying, 
		  CreationDate date,
		  IdTypeCredit integer,
		  Description character varying,
		  IdCustomer integer,
		  Name character varying,
		  LastName character varying,
		  Photo character varying
		 ) AS $$
	
DECLARE
 BEGIN     
      RETURN QUERY
	      SELECT 
		   c.IdCredit as IdCredit, 
		   c.AmountTotal as AmountTotal,
		   c.PayTotal as PayTotal,
		   c.ResidueTotal as ResidueTotal,
		   c.Status as Status,
		   c.CreationDate as CreationDate,
		   c.IdTypeCredit as IdTypeCredit,
		   tc.Description as Description,
		   c.IdCustomer as IdCustomer,
		   cu.Name as Name,
		   cu.LastName as LastName,
		   cu.Photo as Photo
		   FROM esc_credits."credit" AS c 
		   INNER JOIN esc_credits."typecredit" AS tc on c.IdTypeCredit = tc.IdTypeCredit 
		   INNER JOIN esc_credits."customer" AS cu on c.IdCustomer = cu.IdCustomer;

 END;
$$ LANGUAGE plpgsql;

--SELECT esc_credits.fn_list_credits()


CREATE OR REPLACE FUNCTION esc_credits.fn_pay_credits(
	in_credit integer,
	in_amount decimal)
    RETURNS character varying
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$
	DECLARE response VARCHAR(500);
	DECLARE residual DECIMAL;
	DECLARE status_ VARCHAR(50);
 BEGIN
	response:='0000';
  	
	/*Obtener el saldo del credito*/
	residual:=(SELECT ResidueTotal FROM esc_credits."credit"  WHERE IdCredit = in_credit);
	--Raise Notice 'Value: %', residual;
	
	
	/*Validar si el saldo es menor que el monto*/
	IF in_amount > residual THEN	 
		response:='El monto a pagar es mayor al saldo del credito';
		return response;
	END IF;
	
	
	/*Registrar el pago en el detalle*/
	--Raise Notice 'Registrar pago';
	INSERT INTO esc_credits."creditdetailpay"(AmountPay,Factor,CreationDate,IdCredit) 
	VALUES(in_amount,-1,current_date,in_credit);
	
	/*Actualizar los montos del credito*/
	status_:='Abierto';
	IF in_amount = residual THEN	 
		status_:='Pagado';
	END IF;
	
	UPDATE esc_credits."credit"
	SET Status = status_,
		ResidueTotal = ResidueTotal - in_amount,
		PayTotal = PayTotal + in_amount
	WHERE IdCredit = in_credit;
		
		return response;		
		
	EXCEPTION WHEN others then 
	raise notice '% %', SQLERRM, SQLSTATE;
    return SQLSTATE;
 END;
$BODY$;

ALTER FUNCTION esc_credits.fn_pay_credits(integer, decimal)
    OWNER TO postgres;
	
--SELECT esc_credits.fn_pay_credits(1,1000)



CREATE OR REPLACE FUNCTION esc_credits.fn_reverse_credits(
	in_credit integer,
	in_amount decimal)
    RETURNS character varying
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$
	DECLARE response VARCHAR(500);
	DECLARE residual DECIMAL;
	DECLARE status_ VARCHAR(50);
 BEGIN
	response:='0000';
  		
	/*Registrar el pago en el detalle*/
	--Raise Notice 'Registrar pago';
	INSERT INTO esc_credits."creditdetailpay"(AmountPay,Factor,CreationDate,IdCredit) 
	VALUES(in_amount,1,current_date,in_credit);
	
	/*Actualizar los montos del credito*/
	status_:='Abierto';	
	
	UPDATE esc_credits."credit"
	SET Status = status_,
		ResidueTotal = ResidueTotal + in_amount,
		PayTotal = PayTotal - in_amount
	WHERE IdCredit = in_credit;
		
		return response;		
		
	EXCEPTION WHEN others then 
	raise notice '% %', SQLERRM, SQLSTATE;
    return SQLSTATE;
 END;
$BODY$;

ALTER FUNCTION esc_credits.fn_reverse_credits(integer, decimal)
    OWNER TO postgres;
	
--SELECT esc_credits.fn_reverse_credits(1,100)




