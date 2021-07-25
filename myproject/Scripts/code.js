document.addEventListener("DOMContentLoaded", function () {
    var province = document.getElementById("province");
    window.onload = function () {
        $.ajax({
            url:"https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/province",
            headers: {
                'token': '33103c88-ec46-11eb-9388-d6e0030cbbb7',
                'Content-Type': 'application/json'
            },
            method: 'GET',
            dataType: 'json',
            success: function (response) {
                console.log('success: ');
                console.log(response.data);
                var str = "<option selected>Tỉnh thành</option>";
                for (var i = 0; i < response.data.length; i++) {
                    console.log(response.data[i].ProvinceName);
                    str = str + "<option class='provinceID' data-province='" + response.data[i].ProvinceID + "' >" + response.data[i].ProvinceName + "</option>"
                }
                province.innerHTML = str;
            }
        });
    }
}, false)


function changeFunc() {
    var selectBox = document.getElementById("province");
    var selectedValue = selectBox.options[selectBox.selectedIndex].getAttribute('data-province');
    var district = document.getElementById('district');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/district",
        headers: {
            'token': '33103c88-ec46-11eb-9388-d6e0030cbbb7',
            'Content-Type': 'application/json'
        },
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            
            var str = "<option selected>Quận huyện</option>";
            for (var i = 0; i < response.data.length; i++) {
                if (response.data[i].ProvinceID==selectedValue)
                    str = str + "<option class='districtID' data-district='" + response.data[i].DistrictID + "' >" + response.data[i].DistrictName + "</option>"
            }
            district.innerHTML = str;
        }
    });
}


function changeFuncDistrict() {
    var selectBox = document.getElementById("district");
    var selectedValue = selectBox.options[selectBox.selectedIndex].getAttribute('data-district');
    var ward = document.getElementById('ward');
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id=" + selectedValue,
        headers: {
            'token': '33103c88-ec46-11eb-9388-d6e0030cbbb7',
            'Content-Type': 'application/json'
        },
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            var str = "<option selected>Phường xã</option>";
            for (var i = 0; i < response.data.length; i++) {
                str = str + "<option class='wardID' data-ward='" + response.data[i].WardCode + "' >" + response.data[i].WardName + "</option>"
            }
            ward.innerHTML = str;
        }
    });
}


function changeFuncWard() {
    var selectProvince = document.getElementById("province");
    var selectedValueProvince = selectProvince.options[selectProvince.selectedIndex].getAttribute('data-province');
    console.log(selectedValueProvince);

    var selectDistrict = document.getElementById("district");
    var selectedValueDistrict = selectDistrict.options[selectDistrict.selectedIndex].getAttribute('data-district');
    console.log(selectedValueDistrict);

    var selectWard = document.getElementById("ward");
    var selectedValueWard = selectWard.options[selectWard.selectedIndex].getAttribute('data-ward');
    console.log(selectedValueWard);
    $.ajax({
        url: "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/fee",
        headers: {
            'token': '33103c88-ec46-11eb-9388-d6e0030cbbb7'
        },
        data: {
            "service_id": 53321,
            "insurance_value": 1000000,
            "coupon": null,
            "from_province_id": 202,
            "from_district_id": 1449,
            "to_province_id": selectedValueProvince,
            "to_district_id": selectedValueDistrict,
            "to_ward_code": selectedValueWard,
            "height": 30,
            "length": 20,
            "weight": 5000,
            "width": 20
        },
        method: 'GET',
        dataType: 'json',
        success: function (response) {
            console.log('success: ');
            console.log(response.data.total);
            document.getElementById("ShippingFee").value = response.data.total;
        }
    });
}

