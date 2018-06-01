use DB_Vieclam
--Thêm Tài khoản cá nhân
INSERT INTO [TAIKHOAN_CN] VALUES ('1511061217', 'dinhmanh')
INSERT INTO [TAIKHOAN_CN] VALUES ('1511061420', 'phuoctrung')

--Thêm Ngành
INSERT INTO [NGANH] VALUES (N'Khác')
INSERT INTO [NGANH] VALUES (N'Quản Trị Kinh Doanh')
INSERT INTO [NGANH] VALUES (N'IT - Phần Mềm')
INSERT INTO [NGANH] VALUES (N'IT - Phần Cứng')
INSERT INTO [NGANH] VALUES (N'TXây Dựng')
INSERT INTO [NGANH] VALUES (N'Môi Trường')

--Thêm thành phố
INSERT INTO [THANHPHO] VALUES (N'TP.HCM')
INSERT INTO [THANHPHO] VALUES (N'Hà Nội')

SET DATEFORMAT DMY

--Thêm thông tin cá nhân
INSERT INTO [THONGTINCANHAN] VALUES (1, NULL, N'Lê Đình', N'Mạnh', 'Nam','10/2/1997','ledinhmanh@gmail.com','01653220484',N'Bùi Đình Túy, Bình Thạnh',1)
INSERT INTO [THONGTINCANHAN] VALUES (2, NULL, N'Vũ Đình Phước', N'Trung', 'Nam','30/3/1997','vodinhphuoctrung@gmail.com','01653220484', N'Bùi Đình Túy, Bình Thạnh',1)

--Thêm hồ sơ xin việc
INSERT INTO [HOSOXINVIEC] VALUES (1, 2, N'Tốt Nghiệp Đại Học HuTech', N'Chưa có', N'Làm việc nhóm',N'Tiếng Anh, Tiếng Nhật')
INSERT INTO [HOSOXINVIEC] VALUES (2, 2, N'Tốt Nghiệp Đại Học HuTech', N'Chưa có', N'Làm việc nhóm',N'Tiếng Anh, Tiếng Hàn')

--Thêm Tài khoản doanh nghiệp
INSERT INTO [TAIKHOAN_DN] VALUES ('vietinbank@gmail.com','123456')
INSERT INTO [TAIKHOAN_DN] VALUES ('dienmayxanh@gmail.com','123456')
INSERT INTO [TAIKHOAN_DN] VALUES ('cocacola@gmail.com','123456')
INSERT INTO [TAIKHOAN_DN] VALUES ('pepsi@gmail.com','123456')

--Thêm chi tiết doanh nghiệp
INSERT INTO [TTDOANHNGHIEP] VALUES (1,'',N'Ngân Hàng VietinBank','100',N'Là ngân hàng',N'131 Bùi Đình Túy, Bình Thạnh',1,N'Nhiều Phúc lợi')
INSERT INTO [TTDOANHNGHIEP] VALUES (2,'',N'Cửa hàng Điện máy xanh','100',N'Là cửa hàng điện máy',N'123 Phạm Văn Đồng, Gò Vấp',1,N'Nhiều Phúc lợi')
INSERT INTO [TTDOANHNGHIEP] VALUES (3,'',N'Cocacola Việt Nam','100',N'Là công ty nước giải khát',N'131 Xa Lộ Hà Nội, Thủ Đức',2,N'Nhiều Phúc lợi')
INSERT INTO [TTDOANHNGHIEP] VALUES (4,'',N'Pepsi Việt Nam','100',N'Là công ty nước giải khát',N'123 Trường Chinh, Tân Bình',2,N'Nhiều Phúc lợi')

--Thêm phiếu đăng tuyển
INSERT INTO [PHIEUDANGTUYEN] VALUES (1, '10/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (2, '11/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (3, '12/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (4, '13/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (1, '14/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (2, '15/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (3, '16/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (4, '17/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (1, '18/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (2, '19/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (3, '20/5/2018', '12/6/2018')
INSERT INTO [PHIEUDANGTUYEN] VALUES (4, '12/5/2018', '12/6/2018')

--Thêm Chi tiết đăng tuyển
INSERT INTO [CHITIETTUYENDUNG] VALUES (1, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 1, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (2, N'Tuyển dụng Kế Toán',N'Cử Nhân', N'Nhân Viên', 2, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (3, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 3, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Cửa Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (4, N'Tuyển dụng Giám Đốc',N'Thạc Sĩ', N'Giám Đốc', 4, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Giám Đốc của Ngân Hàng chi nhánh Quận 7', N'Có Kinh nghiệm làm việc ở vị trí tương đương trong 2 năm',N'Các bằng cấp liên quan', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (5, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 4, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (6, N'Tuyển dụng Kế Toán',N'Cử Nhân', N'Nhân Viên', 3, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (7, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 2, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Cửa Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (8, N'Tuyển dụng Giám Đốc',N'Thạc Sĩ', N'Giám Đốc', 1, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Giám Đốc của Ngân Hàng chi nhánh Quận 7', N'Có Kinh nghiệm làm việc ở vị trí tương đương trong 2 năm',N'Các bằng cấp liên quan', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (9, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 3, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (10, N'Tuyển dụng Kế Toán',N'Cử Nhân', N'Nhân Viên', 1, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Ngân Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (11, N'Tuyển dụng Bảo Vệ',N'Cử Nhân', N'Nhân Viên', 4, N'123 Phạm Văn Đồng, Gò Vấp', 1, 10000000, 15000000, N'Nhân viên bảo vệ cửa ra vào của Cửa Hàng', N'Có Kinh nghiệm, có ngoại hình và sức khỏe tốt',N'Xử lý tình huống', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
INSERT INTO [CHITIETTUYENDUNG] VALUES (12, N'Tuyển dụng Giám Đốc',N'Thạc Sĩ', N'Giám Đốc', 2, N'123 Phạm Văn Đồng, Gò Vấp', 2, 10000000, 15000000, N'Giám Đốc của Ngân Hàng chi nhánh Quận 7', N'Có Kinh nghiệm làm việc ở vị trí tương đương trong 2 năm',N'Các bằng cấp liên quan', N'Lê Đình Mạnh',N'ledinhmanh@gmail.com','true')
