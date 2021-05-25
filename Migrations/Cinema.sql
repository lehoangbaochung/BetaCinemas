-- Cơ sở dữ liệu Rạp phim
create database Cinema
go
-- Thao tác cơ sở dữ liệu
use Cinema
go
-- Bảng Thành viên
create table MEMBER(
-- Id 
Id int not null primary key identity(0, 1),
-- Họ tên
FullName nvarchar(30) not null,
-- Email
Email char(50) not null,
-- Mật khẩu
Pass char(15) not null,
-- Số điện thoại
PhoneNumber char(15) not null,
-- Ngày sinh
Birthday date not null,
-- Giới tính
Gender bit not null,
-- Ngày đăng ký tài khoản
RegisterDate date not null,
-- Địa chỉ
HomeAddress ntext,
-- Số thẻ CMND/CCCD/Hộ chiếu
CardNumber char(15))
go
-- Bảng Phim
create table MOVIE(
-- Id
Id int not null primary key identity(1, 1),
-- Tựa đề
Title nvarchar(50) not null,
-- Giới thiệu chi tiết
About ntext,
-- Thời lượng (độ dài)
Duration int,
-- Đạo diễn
Director nvarchar(50),
-- Diễn viên
Actor ntext,
-- Quốc gia
Country nvarchar(50),
-- Ngôn ngữ
Lang nvarchar(20),
-- Thể loại
Genre nvarchar(50),
-- Ngày khởi chiếu
ReleaseDate date,
-- Địa chỉ hình ảnh áp phích
PosterUrl text,
-- Địa chỉ video trailer
TrailerUrl text,
-- Trạng thái khởi chiếu
IsShowing bit)
go
-- Bảng Phòng vé
create table ROOM(
-- Id
Id int not null primary key identity(1, 1),
-- Tổng số hàng trong phòng vé
RowTotal int not null,
-- Tổng số cột trong phòng vé
ColumnTotal int not null,
-- Ghi chú
About ntext)
go
-- Bảng Giá vé
create table TICKETPRICE(
-- Id
Id int not null primary key identity(1, 1),
-- Thứ trong tuần
Weekdays int not null,
-- Thời gian bắt đầu
StartTime time,
-- Thời gian kết thúc
EndTime time,
-- Giá vé
Price int not null,
-- Trạng thái ưu tiên (đối tượng mua vé)
IsPriority bit not null,
-- Trạng thái định dạng của phim (2D/3D)
Is2D bit not null)
go
-- Bảng Bài đăng (Bảng tin)
create table POST(
-- Id
Id int not null primary key identity(1, 1),
-- Thời gian đăng bài
PostTime datetime not null,
-- Nội dung
Content ntext not null,
-- Thể loại (Ưu đãi/Bên lề)
IsPreferential bit not null,
-- Địa chỉ liên kết đính kèm
AttachedUrl text,
-- Địa chỉ hình ảnh đính kèm
ImageUrl text)
go
-- Bảng Liên hệ (Phản hồi)
create table CONTACT(
-- Id
Id int not null primary key identity(1, 1),
-- Id thành viên
MemberId int not null,
-- Thời gian liên hệ
SentTime datetime not null,
-- Nội dung
Content ntext not null,
-- Trạng thái trả lời (đã đọc/chưa đọc)
IsReplied bit not null,
-- Liên kết với bảng Thành viên
foreign key (MemberId) references MEMBER(Id))
go
-- Bảng Ghế
create table SEAT(
-- Id
Id int not null primary key identity(1, 1),
-- Id phòng
RoomId int not null,
-- Chỉ mục hàng
RowIndex char(2) not null,
-- Chỉ mục cột
ColumnIndex int not null,
-- Trạng thái đặt chỗ (còn trống/đã đặt)
IsEmpty bit not null,
-- Liên kết với bảng Phòng
foreign key (Id) references ROOM(Id))
go
-- Bảng Suất chiếu
create table SHOWTIME(
-- Id
Id int not null primary key identity(1, 1),
-- Id phòng
RoomId int not null,
-- Thời gian xuất chiếu
ShowTime datetime not null,
-- Trạng thái định dạng của phim (2D/3D)
Is2D bit not null,
-- Liên kết với bảng Phòng
foreign key (Id) references ROOM(Id))
go
-- Bảng Vé
create table TICKET(
-- Id
Id int not null primary key identity(1, 1),
-- Id thành viên
MemberId int not null,
-- Id phim
MovieId int not null,
-- Id phòng
ShowtimeId int not null,
-- Id giá vé
TicketPriceId int not null,
-- Thời gian bán vé
SoldTime datetime not null,
-- Liên kết với bảng Thành viên
foreign key (MemberId) references MEMBER(Id),
-- Liên kết với bảng Phim
foreign key (MovieId) references MOVIE(Id),
-- Liên kết với bảng Suất chiếu
foreign key (ShowtimeId) references SHOWTIME(Id),
-- Liên kết với bảng Giá vé
foreign key (TicketPriceId) references TICKETPRICE(Id))
go
-- Bảng Hoá đơn
create table BILL(
-- Id
Id int not null primary key identity(1, 1),
-- Id thành viên
MemberId int not null,
-- Thời gian thanh toán
SoldTime datetime,
-- Tổng tiền
Total int not null,
-- Trạng thái thanh toán
IsSold bit not null,
-- Ghi chú
About ntext,
-- Liên kết với bảng Thành viên
foreign key (MemberId) references MEMBER(Id))
go
-- Bảng Chi tiết hoá đơn
create table BILLDETAIL(
-- Id
Id int not null primary key identity(1, 1),
-- Id hoá đơn
BillId int not null,
-- Id vé
TicketId int not null,
-- Liên kết với bảng Hoá đơn
foreign key (BillId) references BILL(Id),
-- Liên kết với bảng Vé
foreign key (TicketId) references TICKET(Id))
