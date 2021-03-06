create table dillensqlmanagementstudiouser(
macAdressPC varchar(12) primary key,
qtdTimesUsedApp int not null
)

create table userdatabase(
macAdressPC varchar(12) not null,
conStr varchar(300) not null, --without password
encryptedPassword varchar(256) not null,
qtdExecutions int not null,
qtdSintaxHelp int not null,
wasLast bit not null,

constraint fkMacAdressPC foreign key (macAdressPC) references
dillensqlmanagementstudiouser(macAdressPC), 
constraint pkUserdatabase primary key (macAdressPC, conStr)
)

create table sqlCommand(
codCmd int primary key,
name varchar(30) not null,
justUsedOnHelp bit not null --0: used on commands' list, 1: used on FrmExplanation...
)

create table explanationSqlCommand(
codCmd int not null,
stage int not null,
title varchar(200) not null,
explanation text not null,
textUserTry text not null

constraint pkExplanationSqlCommand 
primary key (codCmd, stage)
)