                                                                /*Step: 1*/
 
 Create database Restaurent_Management_System;
 
                                                                  /*Step: 2*/
  
create table User_Registration
(
Reg_id  int identity(1,1) primary key not null,
UserName varchar(50) not null,
Email varchar(100) not null,
Password varchar(100) not Null ,
Reg_Type varchar(100) not Null,
Gender char(1) not null,
date_time datetime not null
);


create table User_Login
(
Login_id  int identity(1,1) primary key not null,
Email varchar(100) not null,
Password varchar(100) not Null ,
date_time datetime not null
);


create table admin_1
(
admin_id  int identity(1,1) primary key not null,
AdminName varchar(50) not null,
Email varchar(100) not null,
Password varchar(100) not Null ,
Gender char(1) not null,
date_time datetime not null
);


                                                                      /*Step:  3*/

create table User_RegLogin
(
RegLogin_id  int identity(1,1) primary key not null,

Login_id int foreign key REFERENCES User_Login(Login_id)on delete cascade ,
Registraion_id int foreign key REFERENCES User_Registration(Reg_id) on delete cascade  ,
admin_id int foreign key REFERENCES admin_1(admin_id) on delete cascade  ,

date_time datetime not null

);

                                                                     /*Step:  4*/

create procedure spInsertRegLogin 
@UserName varchar(50),
@Email varchar(50),
@Password varchar(50),
@Reg_Type varchar(50),
@Gender char(1)
as
Begin

 Declare @Login_id int
 Declare @Reg_id int
 Declare @admin_id int
 
 Select @Login_id = Login_id from User_Login where Email = @Email AND Password = @Password 
 Select @Reg_id = Reg_id from User_Registration where UserName = @UserName AND Email = @Email AND Password = @Password AND Reg_Type = @Reg_Type and Gender = @Gender 
 Select @admin_id = admin_id from admin_1 where AdminName = @UserName AND Email = @Email AND Password = @Password AND  Gender = @Gender 

 If (@Login_id is null)
 Begin
  Insert into User_Login values(@Email,@Password,CURRENT_TIMESTAMP)
  Select @Login_id = SCOPE_IDENTITY()
 End

 If (@Reg_id is null)
 Begin
  Insert into User_Registration values(@UserName,@Email,@Password,@Reg_Type,@Gender,CURRENT_TIMESTAMP)
  Select @Reg_id = SCOPE_IDENTITY()
 End

 If (@admin_id is null and @Reg_Type = 'Admin')
 Begin
  Insert into admin_1 values(@UserName,@Email,@Password,@Gender,CURRENT_TIMESTAMP)
  Select @admin_id = SCOPE_IDENTITY()
 End


 Insert into User_RegLogin values(@Login_id, @Reg_id,@admin_id,CURRENT_TIMESTAMP)

End



                                                                     /*Step:  5*/


 
create table staff
(
Staff_id  int identity(1,1) primary key not null,
UserName varchar(50) not null,
Email varchar(100) not null,
Password varchar(100) not Null ,
Staff_Type varchar(100) not Null,
Gender char(1) not null ,
staff_Image varchar(max)not null,
date_time datetime not null
);
create table admin_staff
(
admin_staff_id  int identity(1,1) primary key not null,
Staff_id int foreign key REFERENCES staff(Staff_id)on delete cascade ,
admin_id int foreign key REFERENCES admin_1(admin_id) on delete cascade,

date_time datetime not null

);

                                                                     /*Step:  6*/


create procedure spInsert_staff 
@UserName varchar(50),
@Email varchar(50),
@Password varchar(50),
@Staff_Type varchar(50),
@Gender char(1),
@staff_Image varchar(max)

as
Begin

 Declare @Staff_id int
 
 Declare @admin_id int
 
 Select @Staff_id = Staff_id from staff where UserName = @UserName AND Email = @Email AND Password = @Password AND Staff_Type = @Staff_Type and Gender = @Gender and staff_Image=@staff_Image
 Select @admin_id = admin_id from admin_1 where AdminName = @UserName AND Email = @Email AND Password = @Password AND  Gender = @Gender 


 If (@Staff_id is null)
 Begin
  Insert into staff values(@UserName,@Email,@Password,@Staff_Type,@Gender,@staff_Image,CURRENT_TIMESTAMP)
  Select @Staff_id = SCOPE_IDENTITY()
 End

 Insert into admin_staff values(@Staff_id,@admin_id,CURRENT_TIMESTAMP)

End
 

                                                                         /*Step:  7*/
 
 
 
 create table tbl_product
(
pro_Food_id int identity primary key not null,
pro_FoodName  varchar(100) not null unique,
pro_FoodDescription varchar(250) not null,
pro_FoodPrice bigint not null,
pro_dealsName varchar(100)not null,
pro_FoodImage varchar(max)not null,
pro_Date_Time Datetime
);



create table tbl_invoice
(
in_id int identity primary key not null,
in_fk_Login_id int foreign key references User_Login(Login_id)on delete cascade,
in_total_bill float,
in_Date_Time datetime

);
                                                                             /*Step:  8*/
 
create table tbl_order
(
o_id int identity primary key not null,
o_fk_pro_Food_id int foreign key references tbl_product(pro_Food_id)on delete cascade,
o_fk_in_id int foreign key references tbl_invoice(in_id)on delete cascade,
o_Quantity int,
o_bill float,
o_unitprice int,
o_Date_Time datetime

);
                                                                              /*Step:  9*/
         
create table customers
(
cust_id int identity(1,1) primary key not null,

fullName varchar(50) not null,
Email varchar(50) not null,
Address varchar(50) not null,
City varchar(50),
Zip bigint not null,
Date_Time datetime not null
);

create table payment
(
payment_id int identity(1,1) primary key not null,

NameOnCard varchar(100) not null,
CraditCardNumber bigint not null,
EXPMonth varchar(100) not null,
CVV int not null,
EXPYear bigint not null,
Date_Time datetime not null
);
                                                                              /*Step:  10*/
         
create table Customer_payment
(
Customer_payment int identity(1,1) primary key not null,

cust_id int foreign key REFERENCES customers(cust_id)on delete cascade,
payment_id int foreign key REFERENCES payment(payment_id)on delete cascade,
Date_Time datetime not null
);

create table FeedBack
(
FeedBack_id int identity(1,1) primary key not null,
Fisrt_Name varchar(100) not null,
Last_Name varchar(100) not null,
City varchar(100) not null,
Comment varchar(250) not null,
Date_Time datetime not null
);

                                                                              /*Step:  11*/
         
/*For insert Deals store Procedure*/


 create procedure spInsertIntoDeals 
@pro_FoodName varchar(50),
@pro_FoodDescription varchar(250),
@pro_FoodPrice bigint,
@pro_FoodImage varchar(max),
@pro_dealsName varchar(100)

as
Begin
declare @pro_Food_id int
 If (@pro_Food_id is null)
 Begin
  Insert into tbl_product values(@pro_FoodName,@pro_FoodDescription,@pro_FoodPrice,@pro_dealsName,@pro_FoodImage,CURRENT_TIMESTAMP)
  Select @pro_Food_id = SCOPE_IDENTITY()
 End
 
End

/*For Payment store Procedure*/


 create procedure spInsertIntoCutomerPayment 
@fullName varchar(50),
@Email varchar(50),
@Address varchar(50),
@City varchar(50),
@Zip bigint,
@NameOnCard varchar(100),
@CraditCardNumber bigint,
@EXPMonth varchar(100),
@CVV int,
@EXPYear bigint

as
Begin

 Declare @cust_id int
 Declare @payment_id int

 Select @cust_id = cust_id from customers where fullName = @fullName And Email = @Email and Address = @Address and City = @City and Zip = @Zip 
 Select @payment_id = payment_id from payment where NameOnCard = @NameOnCard and CraditCardNumber = @CraditCardNumber 
 and  EXPMonth = @EXPMonth and CVV = @CVV and EXPYear = @EXPYear ;

 If (@cust_id is null)
 Begin
  Insert into customers values(@fullName,@Email,@Address,@City,@Zip,CURRENT_TIMESTAMP)
  Select @cust_id = SCOPE_IDENTITY()
 End

 If (@payment_id is null)
 Begin
  Insert into payment values(@NameOnCard,@CraditCardNumber,@EXPMonth,@CVV,@EXPYear,CURRENT_TIMESTAMP)
  Select @payment_id = SCOPE_IDENTITY()
 End

 Insert into Customer_payment values(@cust_id, @payment_id ,CURRENT_TIMESTAMP)

End

select * from admin_1
select * from User_Registration
select * from User_Login

ALTER TABLE admin_1
drop  Column date_time;

insert into admin_1(AdminName,Email,Password,Gender,date_time) VALUES('admin','admin@gmail.com','12345','m',CURRENT_TIMESTAMP);



insert into User_Registration(UserName,Email,Password,Reg_Type,Gender,date_time) VALUES('admin','admin@gmail.com','12345','admin','m',CURRENT_TIMESTAMP);
insert into User_Login(Email,Password,date_time) VALUES('admin@gmail.com','12345',CURRENT_TIMESTAMP)


insert into admin_1 values('bilal','admin@gmail.com','12345','male')
delete from admin_1 where admin_id =37; 