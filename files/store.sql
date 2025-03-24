DROP TABLE IF EXISTS client CASCADE;
DROP TABLE IF EXISTS orderprod CASCADE ;
DROP TABLE IF EXISTS provider CASCADE ;
DROP TABLE IF EXISTS product CASCADE;
DROP TABLE IF EXISTS salesperson CASCADE;


CREATE TABLE IF NOT EXISTS client ( 
					id			SERIAL NOT NULL PRIMARY KEY,
					code 		NUMERIC (12) NOT NULL,
					name 		VARCHAR (30) NOT NULL,
					credit 		DECIMAL(9,2)
);

INSERT INTO client (code, name, credit) VALUES (2111,'JCP Inc.',50000.00) ;
INSERT INTO client (code, name, credit) VALUES (2102,'First Corp.',65000.00);
INSERT INTO client (code, name, credit) VALUES (2103,'Acme Mfg.',50000.00);
INSERT INTO client (code, name, credit) VALUES (2123,'Carter & Sons',40000.00);
INSERT INTO client (code, name, credit) VALUES (2107,'Ace International',35000.00);
INSERT INTO client (code, name, credit) VALUES (2115,'Smithson Corp.',20000.00);
INSERT INTO client (code, name, credit) VALUES (2101,'Jones Mfg.',65000.00);
INSERT INTO client (code, name, credit) VALUES (2112,'Zetacorp',50000.00);
INSERT INTO client (code, name, credit) VALUES (2121,'QMA Assoc.',45000.00);
INSERT INTO client (code, name, credit) VALUES (2114,'Orion Corp',20000.00);
INSERT INTO client (code, name, credit) VALUES (2124,'Peter Brothers',40000.00);
INSERT INTO client (code, name, credit) VALUES (2108,'Holm & Landis',55000.00);
INSERT INTO client (code, name, credit) VALUES (2117,'J.P. Sinclair',35000.00);
INSERT INTO client (code, name, credit) VALUES (2122,'Three-Way Lines',30000.00);
INSERT INTO client (code, name, credit) VALUES (2120,'Rico Enterprises',50000.00);
INSERT INTO client (code, name, credit) VALUES (2106,'Fred Lewis Corp.',65000.00);
INSERT INTO client (code, name, credit) VALUES (2119,'Solomon Inc.',25000.00);
INSERT INTO client (code, name, credit) VALUES (2118,'Midwest Systems',60000.00);
INSERT INTO client (code, name, credit) VALUES (2113,'Ian & Schmidt',20000.00);
INSERT INTO client (code, name, credit) VALUES (2109,'Chen Associates',25000.00);
INSERT INTO client (code, name, credit) VALUES (2105,'AAA Investments',45000.00);


CREATE TABLE IF NOT EXISTS salesperson ( 
					   id			SERIAL NOT NULL PRIMARY KEY,
					   surname		VARCHAR (25) NOT NULL,
                       job			VARCHAR (50),
                       startdate	TIMESTAMPTZ,
                       salary		DECIMAL(12,2),
                       commission	DECIMAL(12,2),
					   dep  		VARCHAR (25) NOT NULL 
					   );

INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('PENN','MANAGER', '1980-12-17', 104000,NULL,'WOOD');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('CARLSON','ASSISTANT', '1980-02-20', 208000,39000,'GARAGE');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('RUPERT','ASSISTANT', '1981-02-22', 162500,65000,'GARAGE');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('CRAIG','MANAGER', '1981-04-02', 386750,NULL,'DIY');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('STRONGMAN','ASSISTANT', '1981-09-29', 162500,182000,'DIY');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('ROBERTS','ASSISTANT', '1981-05-01', 370500,NULL,'DIY');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('YOUNG','MANAGER', '1981-06-09', 318500,NULL,'HARDWARE');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('SCOTT','MANAGER', '1981-11-09', 390000,NULL,'IT');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('PIPEN','MANAGER', '1981-11-17', 650000,NULL,'REPAIR PART');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('REYES','ASSISTANT', '1981-09-08', 195000,0,'REPAIR PART');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('RALPH','ASSISTANT', '1981-09-23', 143000,NULL,'WOOD');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('NORTON','ASSISTANT', '1981-12-03', 123500,NULL,'REPAIR PART');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('GLOVERS','ASSISTANT', '1981-12-03', 390000,NULL,'REPAIR PART');
INSERT INTO salesperson (surname,job,startdate,salary,commission,dep) VALUES ('PHELPS','ASSISTANT', '1982-01-23', 169000,NULL,'WOOD');


CREATE TABLE IF NOT EXISTS product ( 
					  id			SERIAL NOT NULL PRIMARY KEY,
                      code			NUMERIC (12) NOT NULL,
                      description	VARCHAR (30) NOT NULL,
                      currentstock	NUMERIC (12),
                      minstock		NUMERIC (12),
                      price			DECIMAL(8,2),
					  salesp		INTEGER,
					  CONSTRAINT FK_salesp FOREIGN KEY (salesp) REFERENCES salesperson(id) ON DELETE CASCADE 
					  );


INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (100860, 'Adjustable wrench', 10, 5, 308,10);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (100861, 'Allen key', 10, 5, 15.275,10);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (100870, 'Brace',10,5,40.6,5);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (100871, 'Caulking gun',10,5,33.2,2);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (100890, 'Cutter',10,5,30,2);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (101860, 'Face mask',10,5,548,3);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (101863, 'Gimlet',10,5,124,5);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (102130, 'Hammer',10,5,48,3);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (200376, 'Cutting pliers',10,5,16,5);
INSERT INTO product (code, description, currentstock, minstock,price,salesp)
VALUES (200380, 'Drill',10,5,24,2);



CREATE TABLE IF NOT EXISTS provider  ( 
						id			SERIAL NOT NULL PRIMARY KEY,
						name		VARCHAR (45) NOT NULL,
						address 	VARCHAR (40) NOT NULL,
						city		VARCHAR (30) NOT NULL,
						stcode		VARCHAR (2),
						zipcode		VARCHAR (10) NOT NULL,
						area		NUMERIC(5),
						phone		VARCHAR (10),
						product		INTEGER NOT NULL,
						amount		NUMERIC (12),
						credit		DECIMAL(9,2),
						remark		TEXT,
						CONSTRAINT FK_PROD FOREIGN KEY (product) REFERENCES product(id) ON DELETE CASCADE						
						);				


INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('JOCKSTOOLS', '345 VIEWRIDGE', 'BELMONT', 'CA', '96711', 415,
        '598-6609',5,350,5000,
        'Very friendly people to work with -- sales rep likes to be called Mike.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('CHARLY SHOP', '490 BOLI RD.', 'REDWOOD city', 'CA', '94061', 415,
        '368-1223',6, 752, 10000,
        'Rep called 5/8 about change in order - contact shipping.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('RITEMOUTH', '9722 HAMILTON', 'BURLINGAME', 'CA', '95133', 415,
        '644-3341',2, 800, 7000,
        'Company doing heavy promotion beginning 10/89. Prepare for large orders during winter.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('HAMMER JUST', 'HILLVIEW MALL', 'BURLINGAME', 'CA', '97544', 415,
        '677-9312',7, 400, 3000,
        'Contact rep about new line of tennis rackets.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('EVERY TOOL', '574 SURRY RD.', 'BELMONT', 'CA', '93301', 408,
        '996-2323',8, 250, 10000,
        'CLIENT with high market share (23%) due to aggressive advertising.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('CARLING', '3476 EL PASEO', 'SANTA CLARA', 'CA', '91003', 408,
        '376-9966',9, 625, 5000,
        'Tends to order large amounts of merchandise at once. Accounting is considering raising their credit limit. Usually pays on time.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('DOWN SHAPE', '908 SEQUOIA', 'PALO ALTO', 'CA', '94301', 415,
        '364-9777',10, 280, 6000,
        'Support intensive. Orders small amounts (< 800) of merchandise at a time.');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('GIBBLINGS SHOP', 'VALCO VILLAGE', 'SUNNYVALE', 'CA', '93301', 408,
        '967-4398',1, 2000, 10000,
        'First  goods store geared exclusively towards people. Unusual promotion al style and very willing to take chances towards new products!');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('TRICK SUPPLY', '98 LONE PINE WAY', 'HIBBING', 'MN', '55649', 612,
        '566-9123',3, 800, 8000, '');
INSERT INTO provider  (name,address,city,stcode,zipcode,area,phone, product ,amount, credit, remark)
VALUES ('SOBBERMILL', '13 PERCEBE STR.', 'SPRINGFIELD', 'NT', '0000', 636,
        '999-6666', 4, 700,10000, '');
		
		

CREATE TABLE IF NOT EXISTS orderprod   (
					  id			SERIAL NOT NULL PRIMARY KEY,
                      product		INTEGER,
					  client 		INTEGER,
                      orderdate		TIMESTAMPTZ,
                      amount		NUMERIC (12),
                      deliverydate	TIMESTAMPTZ,
                      cost			DECIMAL(12,2),
					  CONSTRAINT FK_PROV FOREIGN KEY (product ) REFERENCES product (id) ON DELETE CASCADE,
					  CONSTRAINT FK_CLIE FOREIGN KEY (client ) REFERENCES client (id) ON DELETE CASCADE
					  );

INSERT INTO orderprod  (product,client,orderdate,amount,deliverydate,cost)
VALUES (5,10,'1987-01-07', 40, '1987-01-08', 1200);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (7,11,'1987-01-11',100,'1987-01-11', 12400);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (8,8,'1987-01-15',100, '1987-01-20', 4800);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (6,6,'1986-05-01',20, '1986-05-30',10960 );
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (6,7,'1986-06-05', 50, '1986-06-20', 27400);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (10,4,'1986-06-15', 120, '1986-06-30', 2880);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (3,4,'1986-07-14', 130,  '1986-07-30', 5278);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (2,3,'1986-07-14',100, '1986-07-30', 1227.5);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (6,1,'1986-08-01', 100, '1986-08-15', 54800);
INSERT INTO orderprod  (product,client,orderdate,amount,deliverydate,cost)
VALUES (7,5, '1986-07-18', 120,  '1986-07-18', 14880);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (8,4,'1986-07-25', 70, '1986-07-25', 560);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (3,2,'1986-06-05', 94, '1986-06-05', 3816.4);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (9,5, '1987-03-12', 100, '1987-03-12', 1600);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (9,8,'1987-02-01', 110, '1987-02-01', 1760);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (10,11,'1987-02-01', 125, '1987-02-05', 3000);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (6,2,'1987-02-03', 130, '1987-02-10', 71240);
INSERT INTO orderprod  (product,client,orderdate,amount,deliverydate,cost)
VALUES (7,3,'1987-02-22', 200, '1987-02-04', 24800);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (10,3,'1987-02-05',125, '1987-03-03', 3000);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (7,4, '1987-02-01', 60, '1987-02-06', 7440);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (8,2, '1987-02-15', 85, '1987-03-06', 4080);
INSERT INTO orderprod   (product,client,orderdate,amount,deliverydate,cost)
VALUES (10,6, '1987-03-15', 100, '1987-01-01', 2400);

COMMIT;






