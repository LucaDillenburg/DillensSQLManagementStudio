-- Non-Queries
insert into sqlCommand values(0, 'insert into ', 0)
insert into sqlCommand values(1, 'drop table ', 0)
insert into sqlCommand values(2, 'alter table ', 0)
insert into sqlCommand values(3, 'delete from ', 0)
insert into sqlCommand values(4, 'update ', 0)
insert into sqlCommand values(5, 'create table ', 0)
insert into sqlCommand values(6, 'create rule ', 0)
insert into sqlCommand values(7, 'drop view ', 0)
insert into sqlCommand values(8, 'drop proc ', 0)
insert into sqlCommand values(9, 'drop procedure ', 0)
insert into sqlCommand values(10, 'drop function ', 0)
insert into sqlCommand values(11, 'drop trigger ', 0)

-- PROCEDURES, FUNCTIONS, TRIGGER (and view)
insert into sqlCommand values(12, 'create proc ', 0)
insert into sqlCommand values(13, 'create procedure', 0)
insert into sqlCommand values(14, 'alter proc ', 0)
insert into sqlCommand values(15, 'alter procedure ', 0)
insert into sqlCommand values(16, 'create function ', 0)
insert into sqlCommand values(17, 'alter function ', 0)
insert into sqlCommand values(18, 'create trigger ', 0)
insert into sqlCommand values(19, 'alter trigger ', 0)
insert into sqlCommand values(20, 'create view ', 0)
insert into sqlCommand values(21, 'alter view ', 0)

-- other (don't need to use)
insert into sqlCommand values(22, 'exec ', 0)

-- Queries
insert into sqlCommand values(23, 'sp_bindrule ', 0)
insert into sqlCommand values(24, 'sp_unbindrule ', 0)
insert into sqlCommand values(25, 'select ', 0)
insert into sqlCommand values(26, 'sp_help ', 0)

-- other
insert into sqlCommand values(27, 'Execute Procedure', 1)
insert into sqlCommand values(28, 'Execute Function', 1)