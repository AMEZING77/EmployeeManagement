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

