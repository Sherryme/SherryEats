use master
-------------------------------����SherryEats���ݿ�-----------------------------------
if exists(select * from sysdatabases where name='SherryEats')
drop database SherryEats
go
create database SherryEats
on(
name='SherryEats',
filename='E:\SherryEats.mdf',
size=5,
filegrowth=10%
)
log on(
name='SherryEats_log',
filename='E:\SherryEats.ldf',
size=5,
filegrowth=10%
)
---------------------------------------------------------------------------------
go
use SherryEats
go
----------------------------����UserTable�û���----------------------------------
if exists(select * from sysobjects where name='UserTable')
drop table UserTable
go
create table UserTable
(
UserID [int] primary key identity(1,1) not null,  --�û�ID
UserNum [nvarchar](11) unique not null,  --�û��˺�
UserName [nvarchar](7) null,  --�û�����
UserPassWord [nvarchar](11) not null,  --�û�����
Phone [nvarchar](11)  null,  --�û��ֻ���
Address0 [nvarchar](50) null,  --�û�Ĭ�ϵ�ַ
UserType [int] not null  --�û�����
)
--------------------------����MerchantsTable�̼ұ�----------------------------------
go
if exists(select * from sysobjects where name='MerchantsTable')
drop table MerchantsTable
go
create table MerchantsTable
(
MerchantsID [int] primary key identity(1,1) not null,  --�̼�ID
MerchantsName [nvarchar](15) not null,  --��������
MerchantsAddress [nvarchar](50) not null,  --���̵�ַ
SPhone [nvarchar](11)  null,  --�̼ҵ绰
UserID [int] foreign key references UserTable(UserID) null,  --�û�ID
BPhotoName[nvarchar](max)null, --����
SPhotoName[nvarchar](max)null, --ͼƬ����
SPhotoPath[nvarchar](max)null, --ͼƬ·��
MerchantsType [nvarchar](20) null,  --�̼�����
)
--------------------------����FoodTable��Ʒ��Ϣ��----------------------------------
go
if exists(select * from sysobjects where name='FoodTable')
drop table FoodTable
go
create table FoodTable
(
FoodID [int] primary key identity(1,1) not null,  --��ƷID
FoodName [nvarchar](15)  not null,  --��Ʒ����
FoodPrice [decimal](10,2) not null,  --��Ʒ�۸�
FoodIn [nvarchar](50) null,  --��Ʒ����
FoodType [nvarchar](20) not null,  --��Ʒ����
FoodState [nvarchar](15) not null,  --��Ʒ״̬
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --�̼�ID
PhotoName[nvarchar](max)null, --ͼƬ����
PhotoPath[nvarchar](max)null, --ͼƬ·��
HPhotoName[nvarchar](max)null, --ͼƬ
)
--------------------------����ShoppingTable���ﳵ��Ϣ��----------------------------------
go
if exists(select * from sysobjects where name='ShoppingTable')
drop table ShoppingTable
go
create table ShoppingTable
(
ShoppingID [int] primary key identity(1,1) not null,  --���ﳵID
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --�̼�ID
FoodID [int] foreign key references FoodTable(FoodID) not null,  --��ƷID
ShoppingCount [int] null,  --��������
ShoppingPrice [decimal](10,2) not null,  --���ﳵ�����ܶ�
)

--------------------------����TheOrderTable������Ϣ��----------------------------------
go
if exists(select * from sysobjects where name='TheOrderTable')
drop table TheOrderTable
go
create table TheOrderTable
(
OrderID [int] primary key identity(1,1) not null,  --����ID
OrderNumber [nvarchar](50) unique not null,  --�������
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --�̼�ID
UserID [int] foreign key references UserTable(UserID) not null,  --�û�ID
ShoppingPrice [decimal](10,2) not null,  --���ﳵ�����ܶ�
ShoppingNote [nvarchar](50)  null,  --�û���ע
Address0 [nvarchar](50)  not null, --�µ���ַ
SaleDate [nvarchar](50) not null,  --�µ�����
Name [nvarchar](7) null,  --�û�����
XPhone [nvarchar](11)  null,  --�û��ֻ���
)
--------------------------����DetailedTheOrderTable��ϸ������Ϣ��----------------------------------
go
if exists(select * from sysobjects where name='DetailedTheOrderTable')
drop table DetailedTheOrderTable
go
create table DetailedTheOrderTable
(
DetailedID [int] primary key identity(1,1) not null,  --��ϸ����ID
OrderID [int]  foreign key references TheOrderTable(OrderID), --����ID
SFoodName [nvarchar](15)  not null,  --��Ʒ����
FoodPrice [decimal](10,2) not null,  --��Ʒ�۸�
OrderCount [int] not null,  --����

)
--------------------------����OrderStateTable����״̬��----------------------------------
go
if exists(select * from sysobjects where name='OrderStateTable')
drop table OrderStateTable
go
create table OrderStateTable
(
OrderStateID [int] primary key identity(1,1) not null,  --״̬ID
OrderNumber [nvarchar](50) foreign key references TheOrderTable(OrderNumber) not null,  --�������
OrderState [nvarchar](14) not null  --����״̬
)
go
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0)
select '13633758937','sherry',2,'13633758937','������ƽ��������·��ۡ��԰' union
select 'admin','sherry',2,'13633758937','������ƽ��������·��ۡ��԰' 
go
insert into MerchantsTable(MerchantsName,MerchantsAddress,SPhone,UserID,SPhotoName,SPhotoPath,BPhotoName,MerchantsType)
select '������','��Ϫ�����ڽֺ����׷۸���','15972577441',2,'������.jpg','  ','����.png','�ʹ���'union
select '�����ٿ�','ƽ��ɽ��é�����Ϻ�·����С��4��¥102','15972577442',2,'�����ٿ�.jpg','  ','����.png','��ʳ' union
select 'ʳ����','�Ϻ�·����С��101','15972577443',2,'ʳ����.jpg','  ','����.png' ,'��ʳ'union
select 'ʮ��365����','ƽ��ɽ�г���·1��','15972567443',2,'ʮ��365����.jpg','  ','����.png','��ʳ' union
select '����𺺱�','�ʵ������㳡��һ¥','15972567443',2,'����𺺱�.jpg','  ','����.png', '��ʳ'union
select '����ζ��������','é�����������ʱ���Ժ��','14972567443',2,'����ζ��������.jpg','  ','����.png','������ѡ'union
select '���÷���ʽ��Ʒ','����·106����ó��Ժ2��¥101��','14972567441',2,'���÷���ʽ��Ʒ.jpg','  ','����.png','��ʳ'union
select '��ֻ���տ��������','ƽ��ɽ��������������·39���񶨺��߲������г�42������','14972567492',2,'��ֻ���տ��������.jpg','  ','����.png','��ʳ'union
select '��ĥ������','ƽ��ɽ�г�����·24��','14972567856',2,'��ĥ������.jpg','  ','����.png','����'union
select 'Ǳ����Ϻ��','����·55����ҵ��˾����վ��','14102567856',2,'Ǳ����Ϻ��.jpg','  ','����.png','������ѡ'union
select '�ѹ�±Ϻ��','ʮ�߾��ÿ�����������·166��88��','14102512856',2,'�ѹ�±Ϻ��.jpg','  ','����.png','��ʳ'union
select '����������','ƽ��ɽ������·43��1��1-1','14122567856',2,'����������.jpg','  ','����.png','��ʳ'union
select '��������','ƽ��ɽ��é����������·1������������л�԰���25��','12122567856',2,'��������.jpg','  ','����.png','��ѡС��'union
select 'Ԫ�浰���','ƽ��ɽ����¡Բ��һ��1-3-2��','17122567856',2,'Ԫ�浰���.jpeg','  ','����.png','����'union
select '������','���������ڵ��ӵ���¥��2#','12022567856',2,'������.jpg','  ','����.png','�����' union
select '������','ƽ��ɽ��é�����ʵ��5����','12022567856',2,'������.jpg','  ','����.png','�����' union
select '�ʻ���','ƽ��ɽ������·72��','12722567856',2,'�ʻ���.jpeg','  ','����.png','����' union
select '��������','é�����Ϻ�·����С��2��¥101','12722567850',2,'��������.jpg','  ' ,'����.png','��ʳ' union
select '�ϵط�����','ƽ��ɽ��é����������·15�����ҹ�����ָ���','12856567850',2,'�ϵط�����.jpg','  ','����.png','��ʳ' union
select '�ϵ»�լ����','ʮ�߾��ÿ�����������·166��','12884107850',2,'�ϵ»�լ����.jpeg','  ','����.png','�����'  union
select '�����ׯ','�»����Ϻ�·��ѧ�ǳ�ʱ���㳡1��1-11-1��','12896325850',2,'�����ׯ.jpg','  ' ,'����.png','���� 'union
select 'ľľ����','������ѧ3�ż���¥2��Ԫ102','17526325850',2,'ľľ����.jpg','  ' ,'����.png','������ѡ' union
select '������','ƽ��ɽ�б���·����������1��','17526222250',2,'������.jpg','  ','����.png','������ѡ' union
select '�����׼�','ɽ��·�źɴ�����','17511115850',2,'�����׼�.jpg','  ','����.png','������ѡ' union
select '���ܻ��','ƽ��ɽ������·������1�Ű��ü��վƵ�һ¥','17552368850',2,'���ܻ��.jpg','  ','����.png' ,'��ʳ' union
select '���������','ƽ��ɽ������·������1�Ű��ü��վƵ�һ¥','17552396325',2,'���������.jpg','  ','����.png','�����' union
select '�����������','���ֵ߽�ˮ�Ƽ���л�԰·���������Ƶ�10¥','15892396325',2,'�����������.jpg','  ','����.png' ,'������ѡ'union
select 'С����','��������Դ·�����Զ�¥','15892396325',2,'С����.jpg','  ' ,'����.png','�����' union
select 'ũ��С��','����·�����ල����','15892566325',2,'ũ��С��.jpg','  ','����.png' ,'�����' union
select 'ȫȫ��ʳ��','é������վ��·9����Դ��Ʒ1��2��¥��1��4��5������','15892589525',2,'ȫȫ��ʳ��.jpg','  ','����.png','����' union
select '��Ϊ���ʻ�','é��������·34��1¥1-1��','15898526925',2,'��Ϊ���ʻ�.jpeg','  ','����.png' ,'����' union
select '����ƬƤѼ','ƽ��ɽ��տ����������35��','15898789525',2,'����ƬƤѼ.jpg','  ','����.png','��ѡС��'  union
select '������ˮ����','ƽ��ɽ��é��������·�������г�7��15��','15898785215',2,'������ˮ����.jpg','  ','����.png' ,'�ʹ���' union
select '�h���Ͼ�','ƽ��ɽ�г���·258��','15898125615',2,'�h���Ͼ�.jpg','  ' ,'����.png','�����' union
select '365��Ʒˮ����','ƽ��ɽ�����ֳ߽���·1�� ʮ�߹��ʽ������ĸ���','15878925615',2,'365��Ʒˮ����.jpg','  ' ,'����.png','�ʹ���'union
select '���ֻ������','���ְ߽�����·46��','15898185465',2,'���ֻ������.jpg','  ' ,'����.png','����'union
select '���򹱹��½��ز�','���·88�Ŵ�������һ��C��8������','15798185465',2,'���򹱹��½��ز�.jpg','  ','����.png','��ѡС��' union
select 'С�������˴�Ϻ','��Դ·15-3��','15892587465',2,'С�������˴�Ϻ.jpg','  ' ,'����.png','��ѡС��' union
select '��С����Ϻ��','��Դ·188��','15892852465',2,'��С����Ϻ��.jpg','  ' ,'����.png','��ѡС��'
go
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'���������ݲ�����','20.5','','�ҳ�С��','����',2,'���������ݲ�����.jpg',' ','�ޱ���1.png'union
select'����ţ��','32.5','','�ҳ�С��','����',2,'����ţ��.jpg',' ','�ޱ���1.png'union
select'��ʽ�ݲ˿��⳴��','14.5','','�ҳ�С��','����',2,'��ʽ�ݲ˿��⳴��.jpg',' ','�ޱ���1.png'union
select'�ڽ�ţ��֥ʿ�h��','42.5','','�ҳ�С��','����',2,'�ڽ�ţ��֥ʿ�h��.jpg',' ','�ޱ���1.png'union
select'���ƺ�����','35.5','','�ҳ�С��','����',2,'���ƺ�����.jpg',' ','�ޱ���1.png'union
select'����Ϻ��','10.5','','�ҳ�С��','����',2,'����Ϻ��.jpg',' ','�ޱ���1.png'union
select'��ʽ���ʳ��ڶ���','22.5','','�ҳ�С��','����',2,'��ʽ���ʳ��ڶ���.jpg',' ','�ޱ���1.png'union
select'��������˿','33.5','','�ҳ�С��','����',2,'��������˿.jpg',' ','�ޱ���1.png'union
select'����','20.25','','ˮ��','����',1,'����.jpg',' ','�ޱ���1.png'union
select'���ܹ�','26.75','','ˮ��','����',1,'���ܹ�.jpg',' ','�ޱ���1.png'union
select'ƻ��','15.5','','ˮ��','����',1,'ƻ��.jpg',' ','�ޱ���1.png'union
select'����','25','','ˮ��','����',1,'����.jpg',' ','�ޱ���1.png'union
select'�㽶','23','','ˮ��','����',1,'�㽶.jpg',' ','�ޱ���1.png'
go
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0,UserName)
select '�ο�','123456',1,'13633758937','ƽ��ɽ��ƽ��ɽ��ҵְҵ����ѧԺ','�ο�'
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0,UserName)
select '444444','444444',1,'13633758937','ƽ��ɽ��տ����ˮ��·3��','��ѩ��'
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'������','15','','�ҳ�С��','����',3,'������.jpg',' ','�ޱ���1.png'union
select'��Ƥ����+±��','18','','�ҳ�С��','����',3,'��Ƥ����+±��.jpg',' ','�ޱ���1.png'union
select'±��','23','','�ҳ�С��','����',3,'±��.jpg',' ','�ޱ���1.png'union
select'�׷�','5','','�ҳ�С��','����',3,'�׷�.jpg',' ','�ޱ���1.png'
go
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'������Ƥ��Ѽ','20','','�ҳ�С��','����',4,'������Ƥ��Ѽ.jpeg',' ','�ޱ���1.png'union
select'����ըѼ','35.6','','�ҳ�С��','����',4,'����ըѼ.jpg',' ','�ޱ���1.png'union
select'���������','26','','±��','����',4,'���������.jpeg',' ','�ޱ���1.png'union
select'������Ƥ','24.3','','±��','����',4,'������Ƥ.jpg',' ','�ޱ���1.png'union
select'ƬƤ��Ѽ','27.3','','�ҳ�С��','����',4,'ƬƤ��Ѽ.jpeg',' ','�ޱ���1.png'union
select'��ͯ��ר��','40.2','','���','����',5,'��ͯ��ר��.jpg',' ','�ޱ���1.png'union
select'�ɿ�����','46.2','','���','����',5,'�ɿ�����.jpg',' ','�ޱ���1.png'union
select'â��������֬����','80.2','','���','����',5,'â��������֬����.jpg',' ','�ޱ���1.png'union
select'â������','56.3','','�ҳ�С��','����',5,'â������.jpg',' ','�ޱ���1.png'union
select'̤ѩѰݮ','58.3','','�ҳ�С��','����',5,'̤ѩѰݮ.jpg',' ','�ޱ���1.png'union
select'����ë��','18.6','','�ҳ�С��','����',6,'����ë��.jpg',' ','�ޱ���1.png'union
select'����С����','20.56','','�ҳ�С��','����',6,'����С����.jpg',' ','�ޱ���1.png'union
select'���㼦�г�','35.5','','�ҳ�С��','����',6,'���㼦�г�.jpg',' ','�ޱ���1.png'union
select'��������','18.5','','�ҳ�С��','����',6,'��������.jpg',' ','�ޱ���1.png'union
select'����','19','','�ҳ�С��','����',7,'����.jpg',' ','�ޱ���1.png'union
select'��Ƥ','17.5','','�ҳ�С��','����',7,'��Ƥ.jpg',' ','�ޱ���1.png'union
select'�׷�','5.5','','�ҳ�С��','����',7,'�׷�.jpg',' ','�ޱ���1.png'union
select'ũ��С����','18.5','','�ҳ�С��','����',7,'ũ��С����.jpg',' ','�ޱ���1.png'union
select'����Ϻ��','24.3','','�ҳ�С��','����',7,'����Ϻ��.jpg',' ','�ޱ���1.png'union
select'�����׶�����ɺ�','256.3','','�ҳ�С��','����',8,'�����׶�����ɺ�.jpg',' ','�ޱ���1.png'union
select'�����׶����¸ɺ�','356.3','','�ҳ�С��','����',8,'�����׶����¸ɺ�.jpg',' ','�ޱ���1.png'union
select'�����׶���˹�ɺ�','316.3','','�ҳ�С��','����',8,'�����׶���˹�ɺ�.jpg',' ','�ޱ���1.png'union
select'�����׶�����ɺ�','216.3','','�ҳ�С��','����',8,'�����׶�����ɺ�.jpg',' ','�ޱ���1.png'union
select'������ˮ��','15','','�ҳ���','����',9,'������ˮ��.jpg',' ','�ޱ���1.png'union
select'����«����ˮ��','18','','�ҳ���','����',9,'����«����ˮ��.jpg',' ','�ޱ���1.png'union
select'��Ϻˮ��','17','','�ҳ���','����',9,'��Ϻˮ��.jpg',' ','�ޱ���1.png'union
select'�����㹽','15.3','','�ҳ���','����',9,'�����㹽.jpg',' ','�ޱ���1.png'union
select'���Ǵ�','15.3','','ˮ��','����',10,'���Ǵ�.jpg',' ','�ޱ���1.png'union
select'��ݮ�����','24.3','','���','����',10,'��ݮ�����.jpg',' ','�ޱ���1.png'union
select'����������','31.5','','ˮ��','����',10,'����������.jpg',' ','�ޱ���1.png'union
select'̩��������','21.5','','ˮ��','����',10,'̩��������.jpg',' ','�ޱ���1.png'union
select'��Ϫ����','15.3','','���','����',10,'��Ϫ����.jpg',' ','�ޱ���1.png'union
select'������','18.3','','ˮ��','����',11,'������.jpg',' ','�ޱ���1.png'union
select'ţ��ľ������','18.3','','ˮ��','����',11,'ţ��ľ������.jpg',' ','�ޱ���1.png'union
select'���а׻�����','25.3','','ˮ��','����',11,'���а׻�����.jpg',' ','�ޱ���1.png'union
select'���д������','28.3','','ˮ��','����',11,'���д������.jpg',' ','�ޱ���1.png'union
select'���й��ܹ�','24.3','','ˮ��','����',11,'���й��ܹ�.jpg',' ','�ޱ���1.png'union
select'11������õ�廨��','100','','�ҳ�С��','����',12,'11������õ�廨��.jpg',' ','�ޱ���1.png'union
select'19���õ�����','200','','�ҳ�С��','����',12,'19���õ�����.jpg',' ','�ޱ���1.png'union
select'19���õ�廨��','300','','�ҳ�С��','����',12,'19���õ�廨��.jpg',' ','�ޱ���1.png'union
select'21������õ�廨��','320','','�ҳ�С��','����',12,'21������õ�廨��.jpg',' ','�ޱ���1.png'union
select'�ڰ���','130','','�ҳ�С��','����',13,'�ڰ���.jpg',' ','�ޱ���1.png'union
select'�춹޲��ɽҩ��','230','','�ҳ�С��','����',13,'�춹޲��ɽҩ��.jpg',' ','�ޱ���1.png'union
select'����Ƕ���','210','','�ҳ�С��','����',13,'����Ƕ���.jpg',' ','�ޱ���1.png'union
select'��Ͱ����','200','','�ҳ�С��','����',13,'��Ͱ����.jpg',' ','�ޱ���1.png'union
select'��������','21.6','','�ҳ�С��','����',14,'��������.jpg',' ','�ޱ���1.png'union
select'�ඹϺ��','25.6','','�ҳ�С��','����',14,'�ඹϺ��.jpg',' ','�ޱ���1.png'union
select'�ඹ����','28.6','','�ҳ�С��','����',14,'�ඹ����.jpg',' ','�ޱ���1.png'union
select'�ļ�����ĭ','34.6','','�ҳ�С��','����',14,'�ļ�����ĭ.jpg',' ','�ޱ���1.png'union
select'�ɹ���Ƥţ��','24.6','','�ҳ�С��','����',15,'�ɹ���Ƥţ��.jpg',' ','�ޱ���1.png'union
select'������','27.6','','�ҳ�С��','����',15,'������.jpg',' ','�ޱ���1.png'union
select'ˮ����Ƭ','20.6','','�ҳ�С��','����',15,'ˮ����Ƭ.jpg',' ','�ޱ���1.png'union
select'����������','29.6','','�ҳ�С��','����',15,'����������.jpg',' ','�ޱ���1.png'union
select'��о������','24.6','','�ҳ�С��','����',16,'��о������.jpg',' ','�ޱ���1.png'union
select'�ƺӺ����۹�','28.6','','�ҳ�С��','����',16,'�ƺӺ����۹�.jpg',' ','�ޱ���1.png'union
select'����ˮ��ƻ�������','15.6','','�ҳ�С��','����',16,'����ˮ��ƻ�������.jpg',' ','�ޱ���1.png'union
select'�����','20.6','','�ҳ�С��','����',16,'�����.jpg',' ','�ޱ���1.png'union
select'������Ѽ��','22.6','','�ҳ�С��','����',17,'������Ѽ��.jpg',' ','�ޱ���1.png'union
select'�ཷ�����','18.6','','�ҳ�С��','����',17,'�ཷ�����.jpg',' ','�ޱ���1.png'union
select'����ź��','28.6','','�ҳ�С��','����',17,'����ź��.jpg',' ','�ޱ���1.png'union
select'����˿��','25.6','','�ҳ�С��','����',17,'����˿��.jpg',' ','�ޱ���1.png'union
select'�¶�������h��','25.6','','�ҳ�С��','����',18,'�¶�������h��.jpg',' ','�ޱ���1.png'union
select'�ڽ�ţ������','19.6','','�ҳ�С��','����',18,'�ڽ�ţ������.jpg',' ','�ޱ���1.png'union
select'�ݽ�ţ�⳴��','23.6','','�ҳ�С��','����',18,'�ݽ�ţ�⳴��.jpg',' ','�ޱ���1.png'union
select'���������ݷ�','28.6','','�ҳ�С��','����',18,'���������ݷ�.jpeg',' ','�ޱ���1.png'union
select'����','21.6','','�ҳ�С��','����',19,'����.jpg',' ','�ޱ���1.png'union
select'����','18.6','','�ҳ�С��','����',19,'����.jpg',' ','�ޱ���1.png'union
select'���⴮','17.6','','�ҳ�С��','����',19,'���⴮.jpg',' ','�ޱ���1.png'union
select'���лʹ�','98.6','','�ҳ�С��','����',20,'���лʹ�.jpg',' ','�ޱ���1.png'union
select'����ͯ��','186.6','','�ҳ�С��','����',20,'����ͯ��.jpg',' ','�ޱ���1.png'union
select'������Ը','250.6','','�ҳ�С��','����',20,'������Ը.jpg',' ','�ޱ���1.png'union
select'�Ҹ�����','320.6','','�ҳ�С��','����',20,'�Ҹ�����.jpg',' ','�ޱ���1.png'union
select'������','25.6','','�ҳ�С��','����',21,'������.jpg',' ','�ޱ���1.png'union
select'ţ��ľ������','30.6','','�ҳ�С��','����',21,'ţ��ľ������.jpg',' ','�ޱ���1.png'union
select'���а׻�����','38.6','','�ҳ�С��','����',21,'���а׻�����.jpg',' ','�ޱ���1.png'union
select'���д������','18.6','','�ҳ�С��','����',21,'���д������.jpg',' ','�ޱ���1.png'union
select'���й��ܹ�','38.6','','�ҳ�С��','����',21,'���й��ܹ�.jpg',' ','�ޱ���1.png'union
select'������','38.6','','�ҳ�С��','����',22,'������.jpg',' ','�ޱ���1.png'union
select'����','12.6','','�ҳ�С��','����',22,'����.jpg',' ','�ޱ���1.png'union
select'��̳��˳���Ѫ','25.6','','�ҳ�С��','����',22,'��̳��˳���Ѫ.jpg',' ','�ޱ���1.png'union
select'�������','25.6','','�ҳ�С��','����',22,'�������.jpg',' ','�ޱ���1.png'union
select'��Ʒ���ش�Ϻ','123.6','','�ҳ�С��','����',23,'��Ʒ���ش�Ϻ.jpg',' ','�ޱ���1.png'union
select'����Ϻ��','98.6','','�ҳ�С��','����',23,'����Ϻ��.jpg',' ','�ޱ���1.png'union
select'��������Ϻ','135.6','','�ҳ�С��','����',23,'��������Ϻ.jpg',' ','�ޱ���1.png'union
select'�������ش�Ϻ','140.6','','�ҳ�С��','����',23,'�������ش�Ϻ.jpg',' ','�ޱ���1.png'union
select'�����','21.6','','�ҳ�С��','����',24,'�����.jpg',' ','�ޱ���1.png'union
select'С��Ѽ�ƹ�','10.6','','�ҳ�С��','����',24,'С��Ѽ�ƹ�.jpg',' ','�ޱ���1.png'union
select'С��ѼѼ��','23.6','','�ҳ�С��','����',24,'С��ѼѼ��.jpg',' ','�ޱ���1.png'union
select'�㶹��','9.6','','�ҳ�С��','����',24,'�㶹��.jpg',' ','�ޱ���1.png'union
select'�ݽ�����Ƭ��֬�׷�','19.6','','�ҳ�С��','����',25,'�ݽ�����Ƭ��֬�׷�.jpg',' ','�ޱ���1.png'union
select'��������֬�׷�','20.6','','�ҳ�С��','����',25,'��������֬�׷�.jpg',' ','�ޱ���1.png'union
select'������','20.6','','�ҳ�С��','����',26,'������.jpg',' ','�ޱ���1.png'union
select'������','15.6','','�ҳ�С��','����',26,'������.jpg',' ','�ޱ���1.png'union
select'Ƥ����','20.6','','�ҳ�С��','����',26,'Ƥ����.jpg',' ','�ޱ���1.png'union
select'Ǳ����ƷϺ','80.6','','�ҳ�С��','����',26,'Ǳ����ƷϺ.jpg',' ','�ޱ���1.png'union
select'����Ϻβ','90.6','','�ҳ�С��','����',26,'����Ϻβ.jpg',' ','�ޱ���1.png'union
select'���ɵ�VSOP����','150.6','','�ҳ�С��','����',27,'���ɵ�VSOP����.jpg',' ','�ޱ���1.png'union
select'���ɵ°�����XO','230.6','','�ҳ�С��','����',27,'���ɵ°�����XO.jpg',' ','�ޱ���1.png'union
select'���ɵ����������','250.6','','�ҳ�С��','����',27,'���ɵ����������.jpg',' ','�ޱ���1.png'union
select'365�·���','20.6','','�ҳ�С��','����',28,'365�·���.jpg',' ','�ޱ���1.png'union
select'���˫��','30.6','','�ҳ�С��','����',28,'���˫��.jpg',' ','�ޱ���1.png'union
select'�μ���','19.6','','�ҳ�С��','����',28,'�μ���.jpg',' ','�ޱ���1.png'union
select'�������������','25.6','','�ҳ�С��','����',28,'�������������.jpg',' ','�ޱ���1.png'union
select'���Ѵ�Ƥ����+�̲�','25.6','','�ҳ�С��','����',29,'���Ѵ�Ƥ����+�̲�.jpg',' ','�ޱ���1.png'union
select'��ʽ���ǟh��+�̲�','30.6','','�ҳ�С��','����',29,'��ʽ���ǟh��+�̲�.jpg',' ','�ޱ���1.png'union
select'�ڽ�ţ���h��+�̲�','50.6','','�ҳ�С��','����',29,'�ڽ�ţ���h��+�̲�.jpg',' ','�ޱ���1.png'union
select'������Ƥ����+�̲�','40.6','','�ҳ�С��','����',29,'������Ƥ����+�̲�.jpg',' ','�ޱ���1.png'union
select'���Ӽ��ɹ�','20.6','','�ҳ�С��','����',30,'���Ӽ��ɹ�.jpg',' ','�ޱ���1.png'union
select'���������ȸɹ�','20.6','','�ҳ�С��','����',30,'���������ȸɹ�.jpg',' ','�ޱ���1.png'union
select'��������Ϻ','32.6','','�ҳ�С��','����',30,'��������Ϻ.jpg',' ','�ޱ���1.png'union
select'��ζ�ʳ�','40.6','','�ҳ�С��','����',30,'��ζ�ʳ�.jpg',' ','�ޱ���1.png'union
select'���Ĺ�','20.6','','�ҳ�С��','����',31,'���Ĺ�.jpg',' ','�ޱ���1.png'union
select'����','30.6','','�ҳ�С��','����',31,'����.jpg',' ','�ޱ���1.png'union
select'�޻���','40.6','','�ҳ�С��','����',31,'�޻���.jpg',' ','�ޱ���1.png'union
select'����','52.6','','�ҳ�С��','����',31,'����.jpg',' ','�ޱ���1.png'union
select'11֦��õ��C��','98.6','','�ҳ�С��','����',32,'11֦��õ��C��.jpg',' ','�ޱ���1.png'union
select'11֦��õ��L��','123.6','','�ҳ�С��','����',32,'11֦��õ��L��.jpg',' ','�ޱ���1.png'union
select'11֦��õ�����','150.6','','�ҳ�С��','����',32,'11֦��õ�����.jpg',' ','�ޱ���1.png'union
select'66֦��õ��C��','250.6','','�ҳ�С��','����',32,'66֦��õ��C��.jpg',' ','�ޱ���1.png'union
select'������Ϻ','80.6','','�ҳ�С��','����',33,'������Ϻ.jpg',' ','�ޱ���1.png'union
select'�����Ϻ','90.6','','�ҳ�С��','����',33,'�����Ϻ.jpg',' ','�ޱ���1.png'union
select'���˴�Ϻ','70.6','','�ҳ�С��','����',33,'���˴�Ϻ.jpg',' ','�ޱ���1.png'union
select'����Ϻ��','120.6','','�ҳ�С��','����',33,'����Ϻ��.jpg',' ','�ޱ���1.png'union
select'��Ʒ���ش�Ϻ','80.6','','�ҳ�С��','����',34,'��Ʒ���ش�Ϻ.jpg',' ','�ޱ���1.png'union
select'��Ʒ���˴�Ϻ','90.6','','�ҳ�С��','����',34,'��Ʒ���˴�Ϻ.jpg',' ','�ޱ���1.png'union
select'Ǳ����ζ��Ϻ','70.6','','�ҳ�С��','����',34,'Ǳ����ζ��Ϻ.jpg',' ','�ޱ���1.png'union
select'�˳ǿ�ζ��Ϻ','120.6','','�ҳ�С��','����',34,'�˳ǿ�ζ��Ϻ.jpg',' ','�ޱ���1.png'union
select'�����׷�','20.6','','�ҳ�С��','����',35,'�����׷�.jpg',' ','�ޱ���1.png'union
select'�ϸ��走����','25.6','','�ҳ�С��','����',35,'�ϸ��走����.jpg',' ','�ޱ���1.png'union
select'�ܲ�˿���ⶡ����','30.6','','�ҳ�С��','����',35,'�ܲ�˿���ⶡ����.jpg',' ','�ޱ���1.png'union
select'��Ϫ�β˻ع���','31.6','','�ҳ�С��','����',35,'��Ϫ�β˻ع���.jpg',' ','�ޱ���1.png'union
select'�ൺ��ˬ������','25.6','','�ҳ�С��','����',36,'�ൺ��ˬ������.jpg',' ','�ޱ���1.png'union
select'���ش�Ϻ���','30.6','','�ҳ�С��','����',36,'���ش�Ϻ���.jpg',' ','�ޱ���1.png'union
select'���˴�Ϻͬ��','31.6','','�ҳ�С��','����',36,'���˴�Ϻͬ��.jpg',' ','�ޱ���1.png'union
select'����ë��','25.6','','�ҳ�С��','����',37,'����ë��.jpg',' ','�ޱ���1.png'union
select'����С��Ϻ','30.6','','�ҳ�С��','����',37,'����С��Ϻ.jpg',' ','�ޱ���1.png'union
select'���ش�Ϻ','31.6','','�ҳ�С��','����',37,'���ش�Ϻ.jpg',' ','�ޱ���1.png'union
select'���˴�Ϻ','31.6','','�ҳ�С��','����',37,'���˴�Ϻ.jpg',' ','�ޱ���1.png'union
select'���㼦����2��','25.6','','�ҳ�С��','����',38,'���㼦����2��.jpg',' ','�ޱ���1.png'union
select'�����㺺��','30.6','','�ҳ�С��','����',38,'�����㺺��.jpg',' ','�ޱ���1.png'union
select'�����ȱ�','31.6','','�ҳ�С��','����',38,'�����ȱ�.jpg',' ','�ޱ���1.png'union
select'����ţ�Ɽ','31.6','','�ҳ�С��','����',38,'����ţ�Ɽ.jpg',' ','�ޱ���1.png'union
select'6����������Ľ˼','98.6','','�ҳ�С��','����',39,'6����������Ľ˼.jpg',' ','�ޱ���1.png'union
select'��Ϊ����','123.6','','�ҳ�С��','����',39,'��Ϊ����.jpg',' ','�ޱ���1.png'union
select'��������','140.6','','�ҳ�С��','����',39,'��������.jpg',' ','�ޱ���1.png'
go
go
select * from MerchantsTable
go
select * from ShoppingTable
go
select * from UserTable
select * from TheOrderTable
select * from DetailedTheOrderTable
select * from OrderStateTable
go
select * from FoodTable where MerchantsID=3
select * from UserTable

select '0'as FoodID,'��ѡ��'as FoodName union
select FoodID,FoodName from FoodTable


select '0'as MerchantsID,'��ѡ��'as MerchantsName union
select MerchantsID,MerchantsName from MerchantsTable

select * from  TheOrderTable a inner join OrderStateTable b on a.OrderNumber=b.OrderNumber where b.OrderState='�ȴ�ȷ��'
select * from  ShoppingTable where MerchantsID =1 and ShoppingCount>0