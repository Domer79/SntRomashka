use snt33
go

begin tran

--Жилищный фонд снт
create table sector(
	id uniqueidentifier not null primary key,
	number nvarchar(100) not null,
	address nvarchar(300),
	owner uniqueidentifier,
	constraint FK_sector_user foreign key (owner) references [user] (user_id)
)

create index IX_sector_number on sector (number)
go

--Справочник, например: дом, гараж, баня
create table building(
	id uniqueidentifier not null primary key,
	name nvarchar(200) not null,
	area int
)
go

--Постройки в жилищном фонде
create table sector_building(
	sector_id uniqueidentifier not null,
	building_id uniqueidentifier not null,
	build_date datetime2,
	constraint pk_sector_building primary key (sector_id, building_id),
	constraint FK_sector_building__sector foreign key (sector_id) references sector(id),
	constraint FK_sector_building__building foreign key (building_id) references building(id)
)

create unique index UQ_sector_building__created on sector_building (build_date)
go

--Все платежи, которые возникают в снт
create table payment(
	id uniqueidentifier not null primary key,
	name nvarchar(100) not null,
	created datetime2 not null,
	description nvarchar(max)
)
create unique index UQ_payment__created on payment (created)
go

--Платежи в снт, распределенные по каждому участку
create table payment_account(
	id uniqueidentifier not null primary key,
	payment_id uniqueidentifier not null,
	sector_id uniqueidentifier not null,
	debit decimal(10,2),
	debit_date datetime2,
	constraint FK_payment_account__payment foreign key (payment_id) references payment (id),
	constraint FK_payment_account__sector foreign key (sector_id) references sector (id)
)
go

--депозитный счет владельцев участков
create table deposit_account(
	id uniqueidentifier not null primary key,
	sector_id uniqueidentifier not null,
	amount decimal(10,2) default 0,
	constraint FK_deposit_account__sector foreign key (sector_id) references sector (id)
)
go

--Учет движения денег каждого участка
create table deposit_operation(
	id uniqueidentifier not null primary key,
	operation_date datetime2 not null,
	deposit_account_id uniqueidentifier not null,
	operation int not null, -- debit - снт получает денежные средства от владельца, credit - снт выделяет денежные средства владельцу
	amount decimal(10,2) not null,
	constraint FK_deposit_operation__deposit_account foreign key (deposit_account_id) references deposit_account (id)
)
go

commit