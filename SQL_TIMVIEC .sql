﻿CREATE DATABASE DB_Vieclam
USE DB_Vieclam
GO

CREATE TABLE TAIKHOAN_CN
(
	MATKCN INT IDENTITY(1,1) PRIMARY KEY,
	ID VARCHAR(20) UNIQUE NOT NULL,	
	PASS VARCHAR(20) NOT NULL
)
CREATE TABLE TAIKHOAN_DN
(
	MATKDN INT IDENTITY(1,1) PRIMARY KEY,
	ID VARCHAR(20) UNIQUE NOT NULL,
	PASS VARCHAR(20) NOT NULL,
)
CREATE TABLE NGANH
(
	MANGANH INT IDENTITY(1,1) PRIMARY KEY,
	TENNGANH NVARCHAR(50) NOT NULL
)
CREATE TABLE THANHPHO
(
	MATHANHPHO INT IDENTITY(1,1) PRIMARY KEY,
	TENTHANHPHO NVARCHAR(50) NOT NULL,
)
CREATE TABLE THONGTINCANHAN
(
	MATKCN INT PRIMARY KEY,
	HINHDD VARCHAR(100),
	HO NVARCHAR(50),
	TEN NVARCHAR(20),
	GIOITINH NVARCHAR(5),
	NGAYSINH SMALLDATETIME, 
	EMAIL VARCHAR(50),
	SDT VARCHAR (12),
	DIACHI NVARCHAR(100),
	MATHANHPHO INT,
	FOREIGN KEY (MATKCN) REFERENCES TAIKHOAN_CN(MATKCN),
	FOREIGN KEY (MATHANHPHO) REFERENCES THANHPHO(MATHANHPHO),
)
CREATE TABLE HOSOXINVIEC
(
	MATKCN INT PRIMARY KEY, 
	MANGANH INT,
	HOCVAN NVARCHAR(100), 
	KINHNGHIEM NVARCHAR(MAX), 
	KYNANG NVARCHAR(MAX), 
	NGOAINGU NVARCHAR(100), 
	LUONGMM FLOAT,
	FOREIGN KEY (MATKCN) REFERENCES TAIKHOAN_CN(MATKCN),
	FOREIGN KEY (MANGANH) REFERENCES NGANH(MANGANH),
)
CREATE TABLE TTDOANHNGHIEP
(
	MATKDN INT PRIMARY KEY, 
	LOGO VARCHAR(100),
	TENDN NVARCHAR(100) NOT NULL, 
	QUYMODN INT,
	THONGTINDN NVARCHAR(MAX),
	DIACHI NVARCHAR(100), 
	MATHANHPHO INT,
	PHUCLOIDN NVARCHAR(100), 
	FOREIGN KEY (MATKDN) REFERENCES TAIKHOAN_DN(MATKDN),
	FOREIGN KEY (MATHANHPHO) REFERENCES THANHPHO(MATHANHPHO),
)
CREATE TABLE PHIEUDANGTUYEN
(
	MAPDT INT IDENTITY(1,1) PRIMARY KEY,
	MATKDN INT,
	NGAYDANGTIN SMALLDATETIME,
	THOIHANDANGTIN SMALLDATETIME,
	FOREIGN KEY (MATKDN) REFERENCES TAIKHOAN_DN(MATKDN)
)
CREATE TABLE CHITIETTUYENDUNG
(	
	MAPDT INT PRIMARY KEY,	
	TIEUDE NVARCHAR(50) NOT NULL,
	CHUCDANH NVARCHAR(100) NOT NULL,
	CAPBAC NVARCHAR(40) NOT NULL,
	MANGANH INT NOT NULL, 
	DIACHILAMVIEC NVARCHAR(100),
	MATHANHPHO INT NOT NULL,
	MUCLUONGTOITHIEU FLOAT NOT NULL,
	MUCLUONGTOIDA FLOAT NOT NULL,
	MOTACV NVARCHAR(MAX) NOT NULL,
	YEUCAUCV NVARCHAR(MAX) NOT NULL,
	KYNANG NVARCHAR(MAX) NOT NULL,
	NGUOILIENHE NVARCHAR(50) NOT NULL,
	EMAILLIENHE VARCHAR(50) NOT NULL,
	TINHTRANG BIT NOT NULL,
	FOREIGN KEY (MANGANH) REFERENCES NGANH(MANGANH),
	FOREIGN KEY (MAPDT) REFERENCES PHIEUDANGTUYEN(MAPDT),
	FOREIGN KEY (MATHANHPHO) REFERENCES THANHPHO(MATHANHPHO),
)
CREATE TABLE NOPDON
(
	MATKCN INT NOT NULL,
	MAPDT INT NOT NULL,
	NGAYNOPDON SMALLDATETIME,
	TINHTRANG BIT,
	FOREIGN KEY (MATKCN) REFERENCES TAIKHOAN_CN(MATKCN),
	FOREIGN KEY (MAPDT) REFERENCES PHIEUDANGTUYEN(MAPDT) on delete cascade on update cascade,
	PRIMARY KEY(MATKCN,MAPDT)
) 
CREATE TABLE ADMIN
(
	ID VARCHAR(20) PRIMARY KEY,	
	PASS VARCHAR(20) NOT NULL,
)
