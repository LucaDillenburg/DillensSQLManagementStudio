-- CONFIG TABLE: only one row
create table Config(
    -- app, explanation and commands versions (controlled by the date)
    SqlExplanationVersion datetime,
    SqlCommandsVersion datetime,
    DesktopVersion int not null, -- so the API knows which tables were created and their fields (and return the informations accordingly)

	-- to constraint the table to have just one row
	Lock char(1) not null,
    constraint PK_T1 PRIMARY KEY (Lock),
    constraint CK_T1_Locked CHECK (Lock='X')
)

insert into Config(Lock, DesktopVersion) values('X', 1)
-- it doesn't insert the control version of the app and explanation, so the app knows when it's the first time the program is executed and then disables the features that needs at least a version

-- #########################################################################

-- SQL TABLES
create table SqlCommand(
    codCmd int primary key,
    name varchar(30) not null,
    justUsedOnHelp bit not null --0: used on commands' list, 1: used on FrmExplanation...
)

create table ExplanationSqlCommand(
    codCmd int not null,
    stage int not null,
    title varchar(200) not null,
    explanation text not null,
    example text not null

    constraint pkExplanationSqlCommand 
    primary key (codCmd, stage)
)