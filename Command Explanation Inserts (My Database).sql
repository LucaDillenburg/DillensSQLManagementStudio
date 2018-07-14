select * from explanationsqlcommand
delete from explanationsqlcommand

insert into explanationsqlcommand values(codCmd, stage, title, explanation, textUserTry)

-- insert into
insert into explanationsqlcommand values(0, 0, 'General inserts', 'The sintax basic <<*INSERT>> is very simple: \n\r<<bINSERT INTO>> <<_[table name]>> <<bVALUES>>(<<_[value1]>>, <<_[value2]>>, <<_[value3]>>, <<_[value4]>>)\r\nThe sintax must obey the following order:/r/n   1) This values have to be in the same order that the table inserted table./r/n   2) The values must obey the types of them. For example, if <<_[value1]>> is a char or varchar, you will put the text between two single quotation marks. In datetime types you will also put it between single quotation marks. However, you have to follow the following sintax:YYYY-MM-DD HH:mm:SS. If it is an int or any other type, you can just put its value.', 'INSERT INTO [tableName] VALUES([value1], [value2], [value3])')
insert into explanationsqlcommand values(0, 1, 'Complex inserts', 'There is another sintax for INSERTs. The sintax complex <<*INSERT>> is very simple: \r\n<<bINSERT INTO>> <<_[table name]>>(<<_[field1]>>, <<_[field2]>>, <<_[field3]>>) VALUES(<<_[value1]>>, <<_[value2]>>, <<_[value3]>>)\r\nYou can use this sintax when you want, but it is most used when you have null fields, so this way you can not put a field.', 'INSERT INTO [tableName](id, name) VALUES([value1], [value2])')

-- drop table
insert into explanationsqlcommand values(1, 0, 'Drop table', 'The drop table sintax is very simple: <<*DROP TABLE>> <<_[table name]>>', 'DROP TABLE [tableName]')

-- alter table 
insert into explanationsqlcommand values(2, 0, 'Alter table', 'Syntax: ALTER TABLE [table name] {ADD | DROP} {[column name] | CONSTRAINT [constraint]}', 'ALTER TABLE [tableName] ADD COLUMN data datetime')

-- delete from
insert into explanationsqlcommand values(3, 0, 'Delete from', 'Syntax: DELETE FROM [table name] WHERE condition', 'DELETE FROM [tableName] WHERE name = [value2]')

-- update
insert into explanationsqlcommand values(4, 0, 'Update', 'Syntax: UPDATE [table name] SET [column name] = [value] WHERE condition', 'UPDATE [tableName] SET name = [value2] WHERE id = 3')