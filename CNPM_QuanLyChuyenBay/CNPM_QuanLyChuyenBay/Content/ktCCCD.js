
function kiemTraCCCD(index) {
    const cccd = document.getElementById(`CCCD_${index}`).value;

    if (!cccd) {
        alert('Vui lòng nhập CCCD/Passport trước khi kiểm tra.');
        return;
    }

    fetch('/DatVe/KiemTraCCCD', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ cccd: cccd })
    })
        .then(response => response.json())
        .then(data => {
            if (data.status === "found") {
                // Điền thông tin vào form
                document.getElementById(`HoTen_${index}`).value = data.data.HoTen;
                document.getElementById(`GioiTinh_${index}`).value = data.data.GioiTinh;
                document.getElementById(`QuocTich_${index}`).value = data.data.QuocTich;
                document.getElementById(`NgaySinh_${index}`).value = convertAndFormatDate(data.data.NgaySinh.split('T')[0]);

                // Đặt các trường thành readonly
                document.getElementById(`HoTen_${index}`).readOnly = true;
                document.getElementById(`GioiTinh_${index}`).readOnly = true;
                document.getElementById(`QuocTich_${index}`).readOnly = true;
                document.getElementById(`NgaySinh_${index}`).readOnly = true;

                alert('Đã tìm thấy thông tin hành khách.');
            } else {
                alert('CCCD/Passport không tồn tại. Vui lòng nhập thông tin mới.');
                // Bỏ readonly để người dùng nhập lại thông tin
                document.getElementById(`HoTen_${index}`).readOnly = false;
                document.getElementById(`GioiTinh_${index}`).readOnly = false;
                document.getElementById(`QuocTich_${index}`).readOnly = false;
                document.getElementById(`NgaySinh_${index}`).readOnly = false;
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Có lỗi xảy ra khi kiểm tra CCCD.');
        });
}


function convertAndFormatDate(dateString) {
    // Lấy phần số mili giây từ chuỗi
    const miliGiay = dateString.match(/\/Date\((\d+)\)\//)[1];
    const date = new Date(parseInt(miliGiay, 10));

    // Định dạng ngày thành MM/DD/YYYY
    const month = ("0" + (date.getMonth() + 1)).slice(-2);
    const day = ("0" + date.getDate()).slice(-2);
    const year = date.getFullYear();

    return `${month}/${day}/${year}`;
}



