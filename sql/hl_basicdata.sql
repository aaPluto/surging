/*==============================================================*/
/* DB name:      hl_basicdata                                */
/* Created on:     2019/4/21 星期日 1:15:16                        */
/*==============================================================*/

CREATE DATABASE hl_basicdata DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE hl_basicdata;

drop table if exists bd_dictionary;
drop table if exists bd_systemconf;

/*==============================================================*/
/* Table: bd_dictionary                                         */
/*==============================================================*/
create table bd_dictionary
(
   Id                   bigint not null auto_increment comment '主键',
   Code                 varchar(50) not null comment '唯一编码',
   Name                 varchar(50) not null comment '名称',
   ParentId             bigint not null comment '父级Id',
   Seq                  int not null comment '序号',
   SysPreSet          int not null comment '0. 否 1.是',
   TypeName             varchar(50) not null comment '分类名称',
   HasChild             int not null comment '0.没有 1.有',
   CreateBy             bigint comment '创建人',
   CreateTime           datetime comment '创建日期',
   UpdateBy             bigint comment '修改人',
   UpdateTime           datetime comment '修改日期',
   IsDeleted            int comment '软删除标识',
   DeleteBy             bigint comment '删除用户',
   DeleteTime           datetime comment '删除时间',
   primary key (Id)
);

alter table bd_dictionary comment '字典表';

INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Name`, `ParentId`, `Seq`, `SysPreSet`, `TypeName`, `HasChild`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (1, 'systemconf', '系统设置', 0, 1, 1, '系统设置', 1, 0, '2019-05-13 21:25:33', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Name`, `ParentId`, `Seq`, `SysPreSet`, `TypeName`, `HasChild`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (2, 'systemconf_pwd_mode', '密码生成模式', 1, 2, 1, '密码生成模式', 1, 0, '2019-05-13 21:26:04', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Name`, `ParentId`, `Seq`, `SysPreSet`, `TypeName`, `HasChild`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (3, 'systemconf_userpwdmode_random', '随机密码生成模式', 2, 1, 1, '随机密码生成模式', 1, 0, '2019-05-13 21:26:43', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_dictionary`(`Id`, `Code`, `Name`, `ParentId`, `Seq`, `SysPreSet`, `TypeName`, `HasChild`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (4, 'systemconf_userpwdmode_fixed', '固定密码生成模式', 2, 2, 1, '固定密码生成模式', 1, 0, '2019-05-13 21:27:15', NULL, NULL, 0, NULL, NULL);

/*==============================================================*/
/* Table: bd_systemconf                                         */
/*==============================================================*/
create table bd_systemconf
(
   Id                   bigint not null auto_increment comment '主键',
   ConfigName           varchar(50) not null comment '配置名称',
   ConfigValue          varchar(50) not null comment '配置值',
   Memo                 varchar(100) comment '备注',
   Seq                  int not null comment '序号',
   SysPreSet          int not null comment '0. 否 1.是',
   CreateBy             bigint comment '创建人',
   CreateTime           datetime comment '创建日期',
   UpdateBy             bigint comment '修改人',
   UpdateTime           datetime comment '修改日期',
   IsDeleted            int comment '软删除标识',
   DeleteBy             bigint comment '删除用户',
   DeleteTime           datetime comment '删除时间',
   primary key (Id)
);

alter table bd_systemconf comment 'bd_systemconf';

INSERT INTO `hl_basicdata`.`bd_systemconf`(`Id`, `ConfigName`, `ConfigValue`, `Memo`, `Seq`, `SysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (1, 'systemconf_pwd_mode', '1', '密码生成模式', 1, 1, 0, '2019-05-13 21:29:11', NULL, NULL, 0, NULL, NULL);
INSERT INTO `hl_basicdata`.`bd_systemconf`(`Id`, `ConfigName`, `ConfigValue`, `Memo`, `Seq`, `SysPreSet`, `CreateBy`, `CreateTime`, `UpdateBy`, `UpdateTime`, `IsDeleted`, `DeleteBy`, `DeleteTime`) VALUES (2, 'sys_config_pwd_fixed', '123qwe', '固定密码', 2, 1, 0, '2019-05-13 21:30:29', NULL, NULL, 0, NULL, NULL);

