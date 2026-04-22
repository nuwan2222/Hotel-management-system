**--HOTEL ROOM BOOKING SYSTEM**



**--CREATE DATABASE**

**CREATE DATABASE hotel\_booking\_db;**



**--ENTER TO THW DATABASE**

**\\c hotel\_booking\_db;**



**--ROOMTYPE TABLE**



  **create table roomtype (**

    **type\_id serial primary key,**

    **type\_name varchar(50) not null,**

    **description text,**

    **price\_per\_night decimal(10,2) check (price\_per\_night > 0)**

  **);**



**--ROOM TABLE**



   **create table room (**

     **room\_id serial primary key,**

     **room\_number varchar(10) unique not null,**

     **type\_id int references roomtype(type\_id)on delete cascade,**

     **status varchar(20) default 'Available'**

  **);**



**--CUSTOMER TABLE**



  **create table customer(**

     **customer\_id serial primary key,**

     **name varchar(100) not null,**

     **phone varchar(15),**

     **email varchar(100),**

     **address text**

**);**



**-- BOOKING TABLE**



**create table booking (**

    **booking\_id serial primary key,**

    **customer\_id int references customer(customer\_id) on delete cascade,**

    **room\_id int references room(room\_id) on delete cascade,**

    **check\_in date not null,**

    **check\_out date not null,**

    **total\_amount decimal(10,2),**

    **status varchar(20) default 'Pending'**

**);**



**-- PAYMENT KING TABLE**



**create table payment (**

    **payment\_id serial primary key,**

    **booking\_id int references booking(booking\_id) on delete cascade,**

    **amount decimal(10,2) check (amount > 0),**

    **payment\_date date default current\_date,**

    **method varchar(50),**

    **status varchar(20)**

**);**



**-- SERVICE TABLE**



**create table service (**

    **service\_id serial primary key,**

    **service\_name varchar(100),**

    **service\_price decimal(10,2) check (service\_price >= 0)**

**);**





**-- BOOKINGSERVICE TABLE (m:n relation)**



**create table bookingservice (**

    **booking\_id int references booking(booking\_id) on delete cascade,**

    **service\_id INT references service(service\_id) on delete cascade,**

    **quantity INT default 1 check (quantity > 0),**

    **PRIMARY KEY (booking\_id, service\_id)**

**);**



**-- STAFF TABLE**



**create table staff (**

    **staff\_id serial primary key,**

    **name varchar(100),**

    **role varchar(50),**

    **phone varchar(15),**

    **salary decimal(10,2)**

**);**



**-- MAINTENANCE TABLE**



**create table maintenance (**

    **maintenance\_id serial primary key,**

    **room\_id int references room(room\_id) on delete cascade,**

    **staff\_id int references staff(staff\_id) on delete cascade,**

    **maintenance\_date date default current\_date,**

    **description text,**

    **status varchar(20)**

**);**



insert into roomtype (type\_name, description, price\_per\_night)

values ('Deluxe', 'Spacious room with balcony', 150.00),

&nbsp;      ('Standard', 'Basic room with single bed', 80.00);





insert into room (room\_number, type\_id, status)

values ('101', 1, 'Available'),

&nbsp;      ('102', 2, 'Available');



insert into roomtype (type\_name, description, price\_per\_night)

values 

('Deluxe', 'Luxury room with sea view', 150.00),

('Standard', 'Basic room with single bed', 80.00),

('Suite', 'Large suite with two rooms', 250.00);



insert into room (room\_number, type\_id, status)

values 

('105', 1, 'Available'),

('106', 2, 'Available'),

('107', 3, 'Available');



insert into customer (name, phone, email, address)

values 

('nuwan akalanka', '0771234567', 'nuwan@gmail.com', 'dankotuwa'),

('swen jayathunga', '0712345678', 'swen@gmail.com', 'Galle'),

('gushan navven', '07654109678', 'naveen@gmail.com', 'kottawa');



insert into booking (customer\_id, room\_id, check\_in, check\_out, total\_amount, status)

values

(1, 1, '2025-11-01', '2025-11-04', NULL, 'Confirmed'),

(2, 2, '2025-11-03', '2025-11-05', NULL, 'Pending');



insert into service (service\_name, service\_price)

values

('Breakfast', 15.00),

('Laundry', 10.00),

('Airport Pickup', 25.00);



insert into bookingservice (booking\_id, service\_id, quantity)

values

(1, 1, 2),  -- 2 breakfasts

(1, 2, 1),  -- 1 laundry

(2, 3, 1);  -- 1 airport pickup



insert into staff (name, role, phone, salary)

values

('Saman Kumara', 'Cleaner', '0779988776', 45000.00),

('Nimali Silva', 'Receptionist', '0714455667', 60000.00);



insert into maintenance (room\_id, staff\_id, maintenance\_date, description, status)

values

(1, 1, '2025-10-20', 'AC repair', 'Completed'),

(2, 1, '2025-11-01', 'Bathroom leak', 'Ongoing');





insert into booking (customer\_id, room\_id, check\_in, check\_out, total\_amount, status)

values (1, 1, '2025-11-03', '2025-11-05', 10000, 'Confirmed');



insert into booking (customer\_id, room\_id, check\_in, check\_out, total\_amount, status)

VALUES (1, 5, '2025-11-04', '2025-11-06', 12000, 'Confirmed');



insert into room (room\_number, type\_id, status)

values 

('108', 1, 'Available'),

('109', 2, 'Available'),

('110', 3, 'Available');



insert into customer (name, phone, email, address)

values 

('nethmi vijekoon', '0771234467', 'nethmi@gmail.com', 'nuwara');



insert into booking (customer\_id, room\_id, check\_in, check\_out, total\_amount, status)

VALUES (1, 7, '2025-11-04', '2025-11-06', 12000, 'Confirmed'),

(3, 8, '2025-11-04', '2025-11-06', 12000, 'Confirmed'),

(4, 9, '2025-11-04', '2025-11-03', 14000, 'Confirmed')

;



**--FUNCTION 1**

**--CALCULATE TOTAL BOOKING COST**



**create or replace function calculate\_total\_booking\_cost(p\_booking\_id int)**

**returns decimal as $$**

**declare**

    **v\_room\_price decimal;**

    **v\_days int ;**

    **v\_service\_total decimal :=0;**

    **v\_total decimal;**

**begin**

    **select rt.price\_per\_night,**

         **(b.check\_out - b.check\_in)**

    **into v\_room\_price, v\_days**

    **from booking b**

    **join room r on b.room\_id = r.room\_id**

    **join roomtype rt on r.type\_id = rt.type\_id**

    **where b.booking\_id = p\_booking\_id;**



    **select coalesce(sum(s.service\_price \* bs.quantity),0)**

    **into v\_service\_total**

    **from bookingservice bs**

    **join service s on bs.service\_id = s.service\_id**

    **where bs.booking\_id = p\_booking\_id;**



   **v\_total := (v\_room\_price \* v\_days) + v\_service\_total;**



   **return v\_total;**

**end;**

**$$ language plpgsql;**





**--FUNCTION 2**

**--UPDATE ROOM STATUS AUTOMATICALY**

**--working with room\_id in booking**

**--first update the booking table**



**CREATE OR REPLACE FUNCTION update\_room\_status(p\_room\_id INT)**

**RETURNS VOID AS $$**

**BEGIN**

    **-- Check if room has a confirmed booking**

    **IF EXISTS (**

        **SELECT 1 FROM booking**

        **WHERE room\_id = p\_room\_id AND status = 'Confirmed'**

    **) THEN**

        **UPDATE room**

        **SET status = 'Booked'**

        **WHERE room\_id = p\_room\_id;**

    **ELSE**

        **UPDATE room**

        **SET status = 'Available'**

        **WHERE room\_id = p\_room\_id;**

    **END IF;**

**END;**

**$$ LANGUAGE plpgsql;**       







SELECT calculate\_total\_booking\_cost(1);



SELECT update\_room\_status(1);





