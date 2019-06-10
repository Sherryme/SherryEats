use master
-------------------------------创建SherryEats数据库-----------------------------------
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
----------------------------创建UserTable用户表----------------------------------
if exists(select * from sysobjects where name='UserTable')
drop table UserTable
go
create table UserTable
(
UserID [int] primary key identity(1,1) not null,  --用户ID
UserNum [nvarchar](11) unique not null,  --用户账号
UserName [nvarchar](7) null,  --用户姓名
UserPassWord [nvarchar](11) not null,  --用户密码
Phone [nvarchar](11)  null,  --用户手机号
Address0 [nvarchar](50) null,  --用户默认地址
UserType [int] not null  --用户类型
)
--------------------------创建MerchantsTable商家表----------------------------------
go
if exists(select * from sysobjects where name='MerchantsTable')
drop table MerchantsTable
go
create table MerchantsTable
(
MerchantsID [int] primary key identity(1,1) not null,  --商家ID
MerchantsName [nvarchar](15) not null,  --店铺名称
MerchantsAddress [nvarchar](50) not null,  --店铺地址
SPhone [nvarchar](11)  null,  --商家电话
UserID [int] foreign key references UserTable(UserID) null,  --用户ID
BPhotoName[nvarchar](max)null, --标题
SPhotoName[nvarchar](max)null, --图片名称
SPhotoPath[nvarchar](max)null, --图片路径
MerchantsType [nvarchar](20) null,  --商家类型
)
--------------------------创建FoodTable菜品信息表----------------------------------
go
if exists(select * from sysobjects where name='FoodTable')
drop table FoodTable
go
create table FoodTable
(
FoodID [int] primary key identity(1,1) not null,  --菜品ID
FoodName [nvarchar](15)  not null,  --菜品名称
FoodPrice [decimal](10,2) not null,  --菜品价格
FoodIn [nvarchar](50) null,  --菜品介绍
FoodType [nvarchar](20) not null,  --菜品类型
FoodState [nvarchar](15) not null,  --菜品状态
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --商家ID
PhotoName[nvarchar](max)null, --图片名称
PhotoPath[nvarchar](max)null, --图片路径
HPhotoName[nvarchar](max)null, --图片
)
--------------------------创建ShoppingTable购物车信息表----------------------------------
go
if exists(select * from sysobjects where name='ShoppingTable')
drop table ShoppingTable
go
create table ShoppingTable
(
ShoppingID [int] primary key identity(1,1) not null,  --购物车ID
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --商家ID
FoodID [int] foreign key references FoodTable(FoodID) not null,  --菜品ID
ShoppingCount [int] null,  --购物数量
ShoppingPrice [decimal](10,2) not null,  --购物车订单总额
)

--------------------------创建TheOrderTable订单信息表----------------------------------
go
if exists(select * from sysobjects where name='TheOrderTable')
drop table TheOrderTable
go
create table TheOrderTable
(
OrderID [int] primary key identity(1,1) not null,  --订单ID
OrderNumber [nvarchar](50) unique not null,  --订单编号
MerchantsID [int] foreign key references MerchantsTable(MerchantsID) not null,  --商家ID
UserID [int] foreign key references UserTable(UserID) not null,  --用户ID
ShoppingPrice [decimal](10,2) not null,  --购物车订单总额
ShoppingNote [nvarchar](50)  null,  --用户备注
Address0 [nvarchar](50)  not null, --下单地址
SaleDate [nvarchar](50) not null,  --下单日期
Name [nvarchar](7) null,  --用户姓名
XPhone [nvarchar](11)  null,  --用户手机号
)
--------------------------创建DetailedTheOrderTable详细订单信息表----------------------------------
go
if exists(select * from sysobjects where name='DetailedTheOrderTable')
drop table DetailedTheOrderTable
go
create table DetailedTheOrderTable
(
DetailedID [int] primary key identity(1,1) not null,  --详细订单ID
OrderID [int]  foreign key references TheOrderTable(OrderID), --订单ID
SFoodName [nvarchar](15)  not null,  --菜品名称
FoodPrice [decimal](10,2) not null,  --菜品价格
OrderCount [int] not null,  --数量

)
--------------------------创建OrderStateTable订单状态表----------------------------------
go
if exists(select * from sysobjects where name='OrderStateTable')
drop table OrderStateTable
go
create table OrderStateTable
(
OrderStateID [int] primary key identity(1,1) not null,  --状态ID
OrderNumber [nvarchar](50) foreign key references TheOrderTable(OrderNumber) not null,  --订单编号
OrderState [nvarchar](14) not null  --订单状态
)
go
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0)
select '13633758937','sherry',2,'13633758937','信阳市平桥区区府路府邸花园' union
select 'admin','sherry',2,'13633758937','信阳市平桥区区府路府邸花园' 
go
insert into MerchantsTable(MerchantsName,MerchantsAddress,SPhone,UserID,SPhotoName,SPhotoPath,BPhotoName,MerchantsType)
select '果果乐','竹溪县深圳街湖南米粉附近','15972577441',2,'果果乐.jpg','  ','标题.png','鲜果购'union
select '罗曼蒂克','平顶山市茅箭区上海路吉祥小区4号楼102','15972577442',2,'罗曼蒂克.jpg','  ','标题.png','美食' union
select '食力派','上海路吉祥小区101','15972577443',2,'食力派.jpg','  ','标题.png' ,'美食'union
select '十堰365外卖','平顶山市朝阳路1号','15972567443',2,'十堰365外卖.jpg','  ','标题.png','美食' union
select '雨多甜汉堡','邮电街真快活广场负一楼','15972567443',2,'雨多甜汉堡.jpg','  ','标题.png', '美食'union
select '可乐味外卖餐厅','茅箭区三堰物资宾馆院内','14972567443',2,'可乐味外卖餐厅.jpg','  ','标题.png','正餐优选'union
select '贝悦坊法式甜品','中兴路106号外贸大院2号楼101室','14972567441',2,'贝悦坊法式甜品.jpg','  ','标题.png','美食'union
select '三只羊烧烤主题餐厅','平顶山市张湾区汉江南路39号神定河蔬菜批发市场42厂出口','14972567492',2,'三只羊烧烤主题餐厅.jpg','  ','标题.png','美食'union
select '今磨房养生','平顶山市朝阳中路24号','14972567856',2,'今磨房养生.jpg','  ','标题.png','超市'union
select '潜江人虾皇','中兴路55号盐业公司公交站旁','14102567856',2,'潜江人虾皇.jpg','  ','标题.png','正餐优选'union
select '友锅卤虾店','十堰经济开发区白浪中路166号88号','14102512856',2,'友锅卤虾店.jpg','  ','标题.png','美食'union
select '东北饺子王','平顶山市柳林路43号1幢1-1','14122567856',2,'东北饺子王.jpg','  ','标题.png','美食'union
select '三生道场','平顶山市茅箭区北京中路1号香格里拉城市花园马街25号','12122567856',2,'三生道场.jpg','  ','标题.png','精选小吃'union
select '元祖蛋糕店','平顶山市万隆圆方一层1-3-2号','17122567856',2,'元祖蛋糕店.jpeg','  ','标题.png','超市'union
select '米洛洛','红卫教育口电子电器楼下2#','12022567856',2,'米洛洛.jpg','  ','标题.png','下午茶' union
select '果子乐','平顶山市茅箭区邮电街5号旁','12022567856',2,'果子乐.jpg','  ','标题.png','下午茶' union
select '鲜花坊','平顶山市柳林路72号','12722567856',2,'鲜花坊.jpeg','  ','标题.png','超市' union
select '暴走外卖','茅箭区上海路吉祥小区2号楼101','12722567850',2,'暴走外卖.jpg','  ' ,'标题.png','美食' union
select '老地方餐厅','平顶山市茅箭区北京南路15号刘家沟建设局隔壁','12856567850',2,'老地方餐厅.jpg','  ','标题.png','美食' union
select '肯德基宅急送','十堰经济开发区白浪中路166号','12884107850',2,'肯德基宅急送.jpeg','  ','标题.png','下午茶'  union
select '醇真酒庄','新华区上海路大学星城时代广场1层1-11-1号','12896325850',2,'醇真酒庄.jpg','  ' ,'标题.png','超市 'union
select '木木果果','柳林中学3号家属楼2单元102','17526325850',2,'木木果果.jpg','  ' ,'标题.png','正餐优选' union
select '满店香','平顶山市北京路香格里拉马街1号','17526222250',2,'满店香.jpg','  ','标题.png','正餐优选' union
select '尝来巫家','山西路雅荷大厦旁','17511115850',2,'尝来巫家.jpg','  ','标题.png','正餐优选' union
select '歪蛙火锅','平顶山市人民北路滨河巷1号柏悦假日酒店一楼','17552368850',2,'歪蛙火锅.jpg','  ','标题.png' ,'美食' union
select '川湘馋嘴陶','平顶山市人民北路滨河巷1号柏悦假日酒店一楼','17552396325',2,'川湘馋嘴陶.jpg','  ','标题.png','下午茶' union
select '雅乐轩妈妈菜','二堰街道水云间城市花园路口雅乐轩酒店10楼','15892396325',2,'雅乐轩妈妈菜.jpg','  ','标题.png' ,'正餐优选'union
select '小香莲','张湾区开源路八中旁二楼','15892396325',2,'小香莲.jpg','  ' ,'标题.png','下午茶' union
select '农家小厨','柳林路卫生监督局旁','15892566325',2,'农家小厨.jpg','  ','标题.png' ,'下午茶' union
select '全全零食屋','茅箭区车站北路9号桃源御品1期2号楼负1层4、5号商铺','15892589525',2,'全全零食屋.jpg','  ','标题.png','超市' union
select '花为美鲜花','茅箭区东岳路34号1楼1-1号','15898526925',2,'花为美鲜花.jpeg','  ','标题.png' ,'超市' union
select '北京片皮鸭','平顶山市湛河区北渡镇35号','15898789525',2,'北京片皮鸭.jpg','  ','标题.png','精选小吃'  union
select '来益香水果店','平顶山市茅箭区南岳路大臣批发市场7栋15号','15898785215',2,'来益香水果店.jpg','  ','标题.png' ,'鲜果购' union
select '賖店老酒','平顶山市朝阳路258号','15898125615',2,'賖店老酒.jpg','  ' ,'标题.png','下午茶' union
select '365精品水果汇','平顶山市五堰街朝阳路1号 十堰国际金融中心附近','15878925615',2,'365精品水果汇.jpg','  ' ,'标题.png','鲜果购'union
select '百乐汇生活超市','五堰街办柳林路46号','15898185465',2,'百乐汇生活超市.jpg','  ' ,'标题.png','超市'union
select '西域贡果新疆特产','天津路88号大洋五洲一期C区8号商铺','15798185465',2,'西域贡果新疆特产.jpg','  ','标题.png','精选小吃' union
select '小李子油焖大虾','开源路15-3号','15892587465',2,'小李子油焖大虾.jpg','  ' ,'标题.png','精选小吃' union
select '俞小厨红虾馆','开源路188号','15892852465',2,'俞小厨红虾馆.jpg','  ' ,'标题.png','精选小吃'
go
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'柏林猪肉泡菜意面','20.5','','家常小炒','在售',2,'柏林猪肉泡菜意面.jpg',' ','无标题1.png'union
select'菲力牛排','32.5','','家常小炒','在售',2,'菲力牛排.jpg',' ','无标题1.png'union
select'韩式泡菜烤肉炒饭','14.5','','家常小炒','在售',2,'韩式泡菜烤肉炒饭.jpg',' ','无标题1.png'union
select'黑椒牛柳芝士焗饭','42.5','','家常小炒','在售',2,'黑椒牛柳芝士焗饭.jpg',' ','无标题1.png'union
select'金牌红烧肉','35.5','','家常小炒','在售',2,'金牌红烧肉.jpg',' ','无标题1.png'union
select'麻辣虾球','10.5','','家常小炒','在售',2,'麻辣虾球.jpg',' ','无标题1.png'union
select'日式海鲜炒乌冬面','22.5','','家常小炒','在售',2,'日式海鲜炒乌冬面.jpg',' ','无标题1.png'union
select'酸辣土豆丝','33.5','','家常小炒','在售',2,'酸辣土豆丝.jpg',' ','无标题1.png'union
select'菠萝','20.25','','水果','在售',1,'菠萝.jpg',' ','无标题1.png'union
select'哈密瓜','26.75','','水果','在售',1,'哈密瓜.jpg',' ','无标题1.png'union
select'苹果','15.5','','水果','在售',1,'苹果.jpg',' ','无标题1.png'union
select'西瓜','25','','水果','在售',1,'西瓜.jpg',' ','无标题1.png'union
select'香蕉','23','','水果','在售',1,'香蕉.jpg',' ','无标题1.png'
go
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0,UserName)
select '游客','123456',1,'13633758937','平顶山市平顶山工业职业技术学院','游客'
insert into UserTable(UserNum,UserPassWord,UserType,Phone,Address0,UserName)
select '444444','444444',1,'13633758937','平顶山市湛河区水库路3号','陈雪莉'
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'蛋炒饭','15','','家常小炒','在售',3,'蛋炒饭.jpg',' ','无标题1.png'union
select'虎皮鸡蛋+卤干','18','','家常小炒','在售',3,'虎皮鸡蛋+卤干.jpg',' ','无标题1.png'union
select'卤肠','23','','家常小炒','在售',3,'卤肠.jpg',' ','无标题1.png'union
select'米饭','5','','家常小炒','在售',3,'米饭.jpg',' ','无标题1.png'
go
insert into FoodTable(FoodName,FoodPrice,FoodIn,FoodType,FoodState,MerchantsID,PhotoName,PhotoPath,HPhotoName )
select'北京脆皮烤鸭','20','','家常小炒','在售',4,'北京脆皮烤鸭.jpeg',' ','无标题1.png'union
select'北京炸鸭','35.6','','家常小炒','在售',4,'北京炸鸭.jpg',' ','无标题1.png'union
select'凉拌猪耳朵','26','','卤菜','在售',4,'凉拌猪耳朵.jpeg',' ','无标题1.png'union
select'凉拌猪皮','24.3','','卤菜','在售',4,'凉拌猪皮.jpg',' ','无标题1.png'union
select'片皮烤鸭','27.3','','家常小炒','在售',4,'片皮烤鸭.jpeg',' ','无标题1.png'union
select'儿童节专款','40.2','','甜点','在售',5,'儿童节专款.jpg',' ','无标题1.png'union
select'可可鲜裸','46.2','','甜点','在售',5,'可可鲜裸.jpg',' ','无标题1.png'union
select'芒果淡奶乳脂鲜裸','80.2','','甜点','在售',5,'芒果淡奶乳脂鲜裸.jpg',' ','无标题1.png'union
select'芒果在线','56.3','','家常小炒','在售',5,'芒果在线.jpg',' ','无标题1.png'union
select'踏雪寻莓','58.3','','家常小炒','在售',5,'踏雪寻莓.jpg',' ','无标题1.png'union
select'秘制毛豆','18.6','','家常小炒','在售',6,'秘制毛豆.jpg',' ','无标题1.png'union
select'酸辣小黄鱼','20.56','','家常小炒','在售',6,'酸辣小黄鱼.jpg',' ','无标题1.png'union
select'蒜香鸡中翅','35.5','','家常小炒','在售',6,'蒜香鸡中翅.jpg',' ','无标题1.png'union
select'鱼香茄子','18.5','','家常小炒','在售',6,'鱼香茄子.jpg',' ','无标题1.png'union
select'凉面','19','','家常小炒','在售',7,'凉面.jpg',' ','无标题1.png'union
select'凉皮','17.5','','家常小炒','在售',7,'凉皮.jpg',' ','无标题1.png'union
select'米饭','5.5','','家常小炒','在售',7,'米饭.jpg',' ','无标题1.png'union
select'农家小炒肉','18.5','','家常小炒','在售',7,'农家小炒肉.jpg',' ','无标题1.png'union
select'香辣虾球','24.3','','家常小炒','在售',7,'香辣虾球.jpg',' ','无标题1.png'union
select'卡罗雷尔富贵干红','256.3','','家常小炒','在售',8,'卡罗雷尔富贵干红.jpg',' ','无标题1.png'union
select'卡罗雷尔美奥干红','356.3','','家常小炒','在售',8,'卡罗雷尔美奥干红.jpg',' ','无标题1.png'union
select'卡罗雷尔美斯干红','316.3','','家常小炒','在售',8,'卡罗雷尔美斯干红.jpg',' ','无标题1.png'union
select'卡罗雷尔尼洛干红','216.3','','家常小炒','在售',8,'卡罗雷尔尼洛干红.jpg',' ','无标题1.png'union
select'肉三鲜水饺','15','','家常菜','在售',9,'肉三鲜水饺.jpg',' ','无标题1.png'union
select'西葫芦鸡蛋水饺','18','','家常菜','在售',9,'西葫芦鸡蛋水饺.jpg',' ','无标题1.png'union
select'鲜虾水饺','17','','家常菜','在售',9,'鲜虾水饺.jpg',' ','无标题1.png'union
select'猪肉香菇','15.3','','家常菜','在售',9,'猪肉香菇.jpg',' ','无标题1.png'union
select'冰糖脆','15.3','','水果','在售',10,'冰糖脆.jpg',' ','无标题1.png'union
select'草莓冰淇淋','24.3','','甜点','在售',10,'草莓冰淇淋.jpg',' ','无标题1.png'union
select'佳丽酿葡萄','31.5','','水果','在售',10,'佳丽酿葡萄.jpg',' ','无标题1.png'union
select'泰国菠萝蜜','21.5','','水果','在售',10,'泰国菠萝蜜.jpg',' ','无标题1.png'union
select'竹溪粽子','15.3','','甜点','在售',10,'竹溪粽子.jpg',' ','无标题1.png'union
select'榴莲肉','18.3','','水果','在售',11,'榴莲肉.jpg',' ','无标题1.png'union
select'牛奶木瓜鲜切','18.3','','水果','在售',11,'牛奶木瓜鲜切.jpg',' ','无标题1.png'union
select'鲜切白火龙果','25.3','','水果','在售',11,'鲜切白火龙果.jpg',' ','无标题1.png'union
select'鲜切大块西瓜','28.3','','水果','在售',11,'鲜切大块西瓜.jpg',' ','无标题1.png'union
select'鲜切哈密瓜','24.3','','水果','在售',11,'鲜切哈密瓜.jpg',' ','无标题1.png'union
select'11朵香槟玫瑰花束','100','','家常小炒','在售',12,'11朵香槟玫瑰花束.jpg',' ','无标题1.png'union
select'19朵白玫瑰礼盒','200','','家常小炒','在售',12,'19朵白玫瑰礼盒.jpg',' ','无标题1.png'union
select'19朵红玫瑰花束','300','','家常小炒','在售',12,'19朵红玫瑰花束.jpg',' ','无标题1.png'union
select'21朵香槟玫瑰花束','320','','家常小炒','在售',12,'21朵香槟玫瑰花束.jpg',' ','无标题1.png'union
select'黑八珍','130','','家常小炒','在售',13,'黑八珍.jpg',' ','无标题1.png'union
select'红豆薏米山药粉','230','','家常小炒','在售',13,'红豆薏米山药粉.jpg',' ','无标题1.png'union
select'五谷智多星','210','','家常小炒','在售',13,'五谷智多星.jpg',' ','无标题1.png'union
select'早餐八珍粉','200','','家常小炒','在售',13,'早餐八珍粉.jpg',' ','无标题1.png'union
select'炝炒生菜','21.6','','家常小炒','在售',14,'炝炒生菜.jpg',' ','无标题1.png'union
select'青豆虾仁','25.6','','家常小炒','在售',14,'青豆虾仁.jpg',' ','无标题1.png'union
select'青豆玉米','28.6','','家常小炒','在售',14,'青豆玉米.jpg',' ','无标题1.png'union
select'四季豆肉沫','34.6','','家常小炒','在售',14,'四季豆肉沫.jpg',' ','无标题1.png'union
select'干锅带皮牛肉','24.6','','家常小炒','在售',15,'干锅带皮牛肉.jpg',' ','无标题1.png'union
select'酱猪手','27.6','','家常小炒','在售',15,'酱猪手.jpg',' ','无标题1.png'union
select'水煮肉片','20.6','','家常小炒','在售',15,'水煮肉片.jpg',' ','无标题1.png'union
select'蒜蓉西蓝花','29.6','','家常小炒','在售',15,'蒜蓉西蓝花.jpg',' ','无标题1.png'union
select'红芯火龙果','24.6','','家常小炒','在售',16,'红芯火龙果.jpg',' ','无标题1.png'union
select'黄河红心蜜瓜','28.6','','家常小炒','在售',16,'黄河红心蜜瓜.jpg',' ','无标题1.png'union
select'经典水果苹果梨组合','15.6','','家常小炒','在售',16,'经典水果苹果梨组合.jpg',' ','无标题1.png'union
select'麒麟瓜','20.6','','家常小炒','在售',16,'麒麟瓜.jpg',' ','无标题1.png'union
select'高邮咸鸭蛋','22.6','','家常小炒','在售',17,'高邮咸鸭蛋.jpg',' ','无标题1.png'union
select'青椒炒苦瓜','18.6','','家常小炒','在售',17,'青椒炒苦瓜.jpg',' ','无标题1.png'union
select'酸辣藕带','28.6','','家常小炒','在售',17,'酸辣藕带.jpg',' ','无标题1.png'union
select'蒜泥丝瓜','25.6','','家常小炒','在售',17,'蒜泥丝瓜.jpg',' ','无标题1.png'union
select'奥尔良鸡肉焗饭','25.6','','家常小炒','在售',18,'奥尔良鸡肉焗饭.jpg',' ','无标题1.png'union
select'黑椒牛柳炒饭','19.6','','家常小炒','在售',18,'黑椒牛柳炒饭.jpg',' ','无标题1.png'union
select'泡椒牛肉炒饭','23.6','','家常小炒','在售',18,'泡椒牛肉炒饭.jpg',' ','无标题1.png'union
select'酸辣鱿鱼泡饭','28.6','','家常小炒','在售',18,'酸辣鱿鱼泡饭.jpeg',' ','无标题1.png'union
select'鸡翅','21.6','','家常小炒','在售',19,'鸡翅.jpg',' ','无标题1.png'union
select'羊腰','18.6','','家常小炒','在售',19,'羊腰.jpg',' ','无标题1.png'union
select'猪肉串','17.6','','家常小炒','在售',19,'猪肉串.jpg',' ','无标题1.png'union
select'爱尚皇冠','98.6','','家常小炒','在售',20,'爱尚皇冠.jpg',' ','无标题1.png'union
select'岁月童话','186.6','','家常小炒','在售',20,'岁月童话.jpg',' ','无标题1.png'union
select'心语心愿','250.6','','家常小炒','在售',20,'心语心愿.jpg',' ','无标题1.png'union
select'幸福美满','320.6','','家常小炒','在售',20,'幸福美满.jpg',' ','无标题1.png'union
select'榴莲肉','25.6','','家常小炒','在售',21,'榴莲肉.jpg',' ','无标题1.png'union
select'牛奶木瓜鲜切','30.6','','家常小炒','在售',21,'牛奶木瓜鲜切.jpg',' ','无标题1.png'union
select'鲜切白火龙果','38.6','','家常小炒','在售',21,'鲜切白火龙果.jpg',' ','无标题1.png'union
select'鲜切大块西瓜','18.6','','家常小炒','在售',21,'鲜切大块西瓜.jpg',' ','无标题1.png'union
select'鲜切哈密瓜','38.6','','家常小炒','在售',21,'鲜切哈密瓜.jpg',' ','无标题1.png'union
select'地三鲜','38.6','','家常小炒','在售',22,'地三鲜.jpg',' ','无标题1.png'union
select'花饭','12.6','','家常小炒','在售',22,'花饭.jpg',' ','无标题1.png'union
select'老坛酸菜炒猪血','25.6','','家常小炒','在售',22,'老坛酸菜炒猪血.jpg',' ','无标题1.png'union
select'青菜面籽','25.6','','家常小炒','在售',22,'青菜面籽.jpg',' ','无标题1.png'union
select'精品蒜蓉大虾','123.6','','家常小炒','在售',23,'精品蒜蓉大虾.jpg',' ','无标题1.png'union
select'麻辣虾球','98.6','','家常小炒','在售',23,'麻辣虾球.jpg',' ','无标题1.png'union
select'秘制清蒸虾','135.6','','家常小炒','在售',23,'秘制清蒸虾.jpg',' ','无标题1.png'union
select'秘制蒜蓉大虾','140.6','','家常小炒','在售',23,'秘制蒜蓉大虾.jpg',' ','无标题1.png'union
select'乡巴佬','21.6','','家常小炒','在售',24,'乡巴佬.jpg',' ','无标题1.png'union
select'小胡鸭黄瓜','10.6','','家常小炒','在售',24,'小胡鸭黄瓜.jpg',' ','无标题1.png'union
select'小胡鸭鸭脖','23.6','','家常小炒','在售',24,'小胡鸭鸭脖.jpg',' ','无标题1.png'union
select'鱼豆腐','9.6','','家常小炒','在售',24,'鱼豆腐.jpg',' ','无标题1.png'union
select'泡椒土豆片胭脂米饭','19.6','','家常小炒','在售',25,'泡椒土豆片胭脂米饭.jpg',' ','无标题1.png'union
select'西红柿胭脂米饭','20.6','','家常小炒','在售',25,'西红柿胭脂米饭.jpg',' ','无标题1.png'union
select'拌凉面','20.6','','家常小炒','在售',26,'拌凉面.jpg',' ','无标题1.png'union
select'炒花饭','15.6','','家常小炒','在售',26,'炒花饭.jpg',' ','无标题1.png'union
select'皮蛋粥','20.6','','家常小炒','在售',26,'皮蛋粥.jpg',' ','无标题1.png'union
select'潜江精品虾','80.6','','家常小炒','在售',26,'潜江精品虾.jpg',' ','无标题1.png'union
select'岳阳虾尾','90.6','','家常小炒','在售',26,'岳阳虾尾.jpg',' ','无标题1.png'union
select'莱纳德VSOP白兰','150.6','','家常小炒','在售',27,'莱纳德VSOP白兰.jpg',' ','无标题1.png'union
select'莱纳德白兰地XO','230.6','','家常小炒','在售',27,'莱纳德白兰地XO.jpg',' ','无标题1.png'union
select'莱纳德新锐白兰地','250.6','','家常小炒','在售',27,'莱纳德新锐白兰地.jpg',' ','无标题1.png'union
select'365下饭菜','20.6','','家常小炒','在售',28,'365下饭菜.jpg',' ','无标题1.png'union
select'香锅双菌','30.6','','家常小炒','在售',28,'香锅双菌.jpg',' ','无标题1.png'union
select'盐煎肉','19.6','','家常小炒','在售',28,'盐煎肉.jpg',' ','无标题1.png'union
select'郧阳大碗臭豆腐','25.6','','家常小炒','在售',28,'郧阳大碗臭豆腐.jpg',' ','无标题1.png'union
select'番茄脆皮鸡饭+奶茶','25.6','','家常小炒','在售',29,'番茄脆皮鸡饭+奶茶.jpg',' ','无标题1.png'union
select'港式鸡扒焗饭+奶茶','30.6','','家常小炒','在售',29,'港式鸡扒焗饭+奶茶.jpg',' ','无标题1.png'union
select'黑椒牛柳焗饭+奶茶','50.6','','家常小炒','在售',29,'黑椒牛柳焗饭+奶茶.jpg',' ','无标题1.png'union
select'香辣脆皮鸡饭+奶茶','40.6','','家常小炒','在售',29,'香辣脆皮鸡饭+奶茶.jpg',' ','无标题1.png'union
select'辣子鸡干锅','20.6','','家常小炒','在售',30,'辣子鸡干锅.jpg',' ','无标题1.png'union
select'香辣美人腿干锅','20.6','','家常小炒','在售',30,'香辣美人腿干锅.jpg',' ','无标题1.png'union
select'香辣鱿鱼虾','32.6','','家常小炒','在售',30,'香辣鱿鱼虾.jpg',' ','无标题1.png'union
select'湘味肥肠','40.6','','家常小炒','在售',30,'湘味肥肠.jpg',' ','无标题1.png'union
select'开心果','20.6','','家常小炒','在售',31,'开心果.jpg',' ','无标题1.png'union
select'松子','30.6','','家常小炒','在售',31,'松子.jpg',' ','无标题1.png'union
select'无花果','40.6','','家常小炒','在售',31,'无花果.jpg',' ','无标题1.png'union
select'杏仁','52.6','','家常小炒','在售',31,'杏仁.jpg',' ','无标题1.png'union
select'11枝红玫瑰C款','98.6','','家常小炒','在售',32,'11枝红玫瑰C款.jpg',' ','无标题1.png'union
select'11枝红玫瑰L款','123.6','','家常小炒','在售',32,'11枝红玫瑰L款.jpg',' ','无标题1.png'union
select'11枝红玫瑰礼盒','150.6','','家常小炒','在售',32,'11枝红玫瑰礼盒.jpg',' ','无标题1.png'union
select'66枝粉玫瑰C款','250.6','','家常小炒','在售',32,'66枝粉玫瑰C款.jpg',' ','无标题1.png'union
select'秘制蒸虾','80.6','','家常小炒','在售',33,'秘制蒸虾.jpg',' ','无标题1.png'union
select'蒜香大虾','90.6','','家常小炒','在售',33,'蒜香大虾.jpg',' ','无标题1.png'union
select'油焖大虾','70.6','','家常小炒','在售',33,'油焖大虾.jpg',' ','无标题1.png'union
select'油焖虾球','120.6','','家常小炒','在售',33,'油焖虾球.jpg',' ','无标题1.png'union
select'精品蒜蓉大虾','80.6','','家常小炒','在售',34,'精品蒜蓉大虾.jpg',' ','无标题1.png'union
select'精品油焖大虾','90.6','','家常小炒','在售',34,'精品油焖大虾.jpg',' ','无标题1.png'union
select'潜江口味大虾','70.6','','家常小炒','在售',34,'潜江口味大虾.jpg',' ','无标题1.png'union
select'宜城口味大虾','120.6','','家常小炒','在售',34,'宜城口味大虾.jpg',' ','无标题1.png'union
select'苞谷米饭','20.6','','家常小炒','在售',35,'苞谷米饭.jpg',' ','无标题1.png'union
select'老干妈蛋炒饭','25.6','','家常小炒','在售',35,'老干妈蛋炒饭.jpg',' ','无标题1.png'union
select'萝卜丝腊肉丁炒饭','30.6','','家常小炒','在售',35,'萝卜丝腊肉丁炒饭.jpg',' ','无标题1.png'union
select'竹溪盐菜回锅肉','31.6','','家常小炒','在售',35,'竹溪盐菜回锅肉.jpg',' ','无标题1.png'union
select'青岛清爽易拉罐','25.6','','家常小炒','在售',36,'青岛清爽易拉罐.jpg',' ','无标题1.png'union
select'蒜蓉大虾外地','30.6','','家常小炒','在售',36,'蒜蓉大虾外地.jpg',' ','无标题1.png'union
select'油焖大虾同城','31.6','','家常小炒','在售',36,'油焖大虾同城.jpg',' ','无标题1.png'union
select'麻辣毛豆','25.6','','家常小炒','在售',37,'麻辣毛豆.jpg',' ','无标题1.png'union
select'麻辣小龙虾','30.6','','家常小炒','在售',37,'麻辣小龙虾.jpg',' ','无标题1.png'union
select'蒜蓉大虾','31.6','','家常小炒','在售',37,'蒜蓉大虾.jpg',' ','无标题1.png'union
select'油焖大虾','31.6','','家常小炒','在售',37,'油焖大虾.jpg',' ','无标题1.png'union
select'麦香鸡柳堡2个','25.6','','家常小炒','在售',38,'麦香鸡柳堡2个.jpg',' ','无标题1.png'union
select'三文鱼汉堡','30.6','','家常小炒','在售',38,'三文鱼汉堡.jpg',' ','无标题1.png'union
select'至尊鸡腿堡','31.6','','家常小炒','在售',38,'至尊鸡腿堡.jpg',' ','无标题1.png'union
select'至尊牛肉堡','31.6','','家常小炒','在售',38,'至尊牛肉堡.jpg',' ','无标题1.png'union
select'6号恋爱的心慕思','98.6','','家常小炒','在售',39,'6号恋爱的心慕思.jpg',' ','无标题1.png'union
select'花为名鲜','123.6','','家常小炒','在售',39,'花为名鲜.jpg',' ','无标题1.png'union
select'心心物语','140.6','','家常小炒','在售',39,'心心物语.jpg',' ','无标题1.png'
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

select '0'as FoodID,'请选择'as FoodName union
select FoodID,FoodName from FoodTable


select '0'as MerchantsID,'请选择'as MerchantsName union
select MerchantsID,MerchantsName from MerchantsTable

select * from  TheOrderTable a inner join OrderStateTable b on a.OrderNumber=b.OrderNumber where b.OrderState='等待确认'
select * from  ShoppingTable where MerchantsID =1 and ShoppingCount>0