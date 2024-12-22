#  SQL SERVER Tutorial
创建时间: 2024年12月19日 21:35
Tag: Website
[Link to Youtube](https://youtu.be/JLeaM8pK8dE?si=jUFdx5SOdO2StNMy)
![alt text](<assets/SQL SERVER/image.png>)
##  Connecting to SQL Server
1. 通过SSMS连接数据库
![alt text](<assets/SQL SERVER/image-1.png>)
2. 什么是SSMS
通过本地的SSMS来连接（本地/远端）数据库
![alt text](<assets/SQL SERVER/image-2.png>)
---
## Creating,altering and dropping a database
1. 创建数据库
    ```SQL 
   Create DataBase 数据库名称;
   ```
2. 创建数据库会生成两个文件.mdf和_log.ldf
   ![alt text](<assets/SQL SERVER/image-3.png>) 
3. 修改数据库名称
    ```SQL
    Alter DataBase 数据库名称 Modify Name= 新数据库名称;
    Execute sp_renameDB '数据库名称','新数据库名称';
    ```
4. 删除数据库
    ```SQL
    Drop DataBase 数据库名称
    ```
    ![alt text](<assets/SQL SERVER/image-8.png>)
5. 如果数据库正在使用，断开链接，切换到其他数据库；
    Put the database into single user mode.
    ```SQL 
    Alter DataBase 数据库名称 Set Single_User With Rollback Immediate 
    ```
---
## Creating and working with tables
1. 创建Table;
    ```SQL
    Create Table 数据库名称.dbo.表名(
        ID int NOT NULL Primary Key,
        Email nvarchar(50) NULL,
        Gender nvarchar(50) NOT NULL,
    );   
    ```
    ![alt text](<assets/SQL SERVER/image-7.png>)
2. 建立外键约束
    ```SQL
    Alter table ForeignKeyTable add constraint FK_ForeignKeyTable_Column 
    foreign key (Column) references PrimaryKeyTable(Column);
    ```
3. 为什么要使用外键约束
![alt text](<assets/SQL SERVER/image-4.png>)
![alt text](<assets/SQL SERVER/image-9.png>)
---
## Add Defalut constraint
1. 在几种不同的情况下增加constraint
![alt text](<assets/SQL SERVER/image-5.png>)
2. SSMS提供的template
![alt text](<assets/SQL SERVER/image-6.png>)
---
## Cascading referential intergrity(**)
``
级联引用完整性（Cascading Referential Integrity）是一种数据库约束，用于维护表之间的关系一致性。
当主表中的记录被更新或删除时，级联操作会自动更新或删除引用表中的相关记录。
这样可以确保数据的一致性和完整性。
``
1. 为什么要使用级联引用完整性？
![alt text](<assets/SQL SERVER/image-10.png>)
1. 几种默认的Action
![alt text](<assets/SQL SERVER/image-12.png>)
1. 如何理解外键约束的几种配置操作
在SQL Server中，外键约束可以配置几种默认的操作（Action），
这些操作定义了当主表中的记录被更新或删除时，引用表中的相关记录应该如何处理。
常见的操作包括：
   - `NO ACTION`：如果试图删除或更新主表中的记录，而引用表中存在相关记录，则操作会失败。这是默认行为。
   - `CASCADE`：当主表中的记录被删除或更新时，引用表中的相关记录也会被自动删除或更新。
   - `SET NULL`：当主表中的记录被删除或更新时，引用表中的相关记录的外键列会被设置为NULL。
   - `SET DEFAULT`：当主表中的记录被删除或更新时，引用表中的相关记录的外键列会被设置为默认值。
---
## CHECK constraint
``CHECK约束用于在插入或更新数据时验证列中的值是否符合指定的条件。
如果数据不符合条件，则插入或更新操作将失败。
CHECK约束可以应用于单个列或多个列``
1. 如何添加CHECK constraint
![alt text](<assets/SQL SERVER/image-13.png>)
![alt text](<assets/SQL SERVER/image-14.png>)
---
## Identity Column(***)
``Identity列是一种特殊的列，用于自动生成唯一的标识符。``
1. 如何创建Identity Column
```SQL
Create Table 表名(
    ID int Identity(1,1) Primary Key,
);
```
2. 重置Identity与获取
   - 首先获取当前的种子值
   ```SQL
   SELECT IDENT_CURRENT('表名') AS CurrentIdentity;
   ```
   - 其次确认要重置的种子值不能与原有的冲突；
   ```SQL
   DBCC CHECKIDENT ('表名', RESEED, CurrentIdentity+1);
   ```
3. SCOPE_IDENTITY()与@@IDENTITY与IDENTITY_CURRENT('表名')的区别
    - `SCOPE_IDENTITY()`：返回在当前作用域内最后生成的标识值。(same session and same scope)
    - `@@IDENTITY`:返回最后生成的标识值，不受作用域限制。(same session and any scope)
    - `IDENTITY_CURRENT('表名')`：返回指定表的最后生成的标识值，不受作用域限制。(any session and any scope)
