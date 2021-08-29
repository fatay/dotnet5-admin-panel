$(document).ready(function () {

    /* DataTable starts here. */

    const dataTable = $('#usersTable').DataTable({
        dom:
            "<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd"
                },
                className: 'btn btn-success',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-warning',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/User/GetAllUsers',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data); // Serialize Data
                            dataTable.clear(); // Clear DataTable
                            if (userListDto.ResultStatus === 0) {  // 0 => Operation Success, 1 => Operation Fail.
                                $.each(userListDto.Users.$values, function (index, user) {
                                    dataTable.row.add([
                                        user.Id,
                                        user.UserName,
                                        user.Email,
                                        user.PhoneNumber,
                                        `<img src="/img/${user.Picture}" class="my-image-table" alt="${user.Picture}"/>`,
                                        `<button class="btn btn-primary btn-edit btn-sm" data-id="${user.Id}"><span class="fas fa-edit"></span></button>
                                         <button class="btn btn-danger btn-delete btn-sm" data-id="${user.Id}"><span class="fas fa-minus-circle"></span></button>`
                                    ]);
                                    dataTable.draw(); // Draw & Build DataTable.
                                });
                                $('.spinner-border').hide();
                                $('#usersTable').fadeIn(1500);
                            } else {
                                toastr.error(`${userListDto.Message}`, 'Hata!');
                            }
                        },
                        error: function (err) {
                            console.error(err);
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!');
                        }
                    });
                }
            }
        ],
        language: {
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoThousands": ".",
            "lengthMenu": "Sayfada _MENU_ kayıt göster",
            "loadingRecords": "Yükleniyor...",
            "processing": "İşleniyor...",
            "search": "Ara:",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": {
                "first": "İlk",
                "last": "Son",
                "next": "Sonraki",
                "previous": "Önceki"
            },
            "aria": {
                "sortAscending": ": artan sütun sıralamasını aktifleştir",
                "sortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "1": "1 kayıt seçildi",
                    "0": "-"
                },
                "0": "-",
                "1": "%d satır seçildi",
                "2": "-",
                "_": "%d satır seçildi",
                "cells": {
                    "1": "1 hücre seçildi",
                    "_": "%d hücre seçildi"
                },
                "columns": {
                    "1": "1 sütun seçildi",
                    "_": "%d sütun seçildi"
                }
            },
            "autoFill": {
                "cancel": "İptal",
                "fillHorizontal": "Hücreleri yatay olarak doldur",
                "fillVertical": "Hücreleri dikey olarak doldur",
                "info": "-",
                "fill": "Bütün hücreleri <i>%d<\/i> ile doldur"
            },
            "buttons": {
                "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                "colvis": "Sütun görünürlüğü",
                "colvisRestore": "Görünürlüğü eski haline getir",
                "copySuccess": {
                    "1": "1 satır panoya kopyalandı",
                    "_": "%ds satır panoya kopyalandı"
                },
                "copyTitle": "Panoya kopyala",
                "csv": "CSV",
                "excel": "Excel",
                "pageLength": {
                    "-1": "Bütün satırları göster",
                    "1": "-",
                    "_": "%d satır göster"
                },
                "pdf": "PDF",
                "print": "Yazdır",
                "copy": "Kopyala",
                "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın."
            },
            "infoPostFix": "-",
            "searchBuilder": {
                "add": "Koşul Ekle",
                "button": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "condition": "Koşul",
                "conditions": {
                    "date": {
                        "after": "Sonra",
                        "before": "Önce",
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "number": {
                        "between": "Arasında",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "gt": "Büyüktür",
                        "gte": "Büyük eşittir",
                        "lt": "Küçüktür",
                        "lte": "Küçük eşittir",
                        "not": "Değildir",
                        "notBetween": "Dışında",
                        "notEmpty": "Dolu"
                    },
                    "string": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "endsWith": "İle biter",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "startsWith": "İle başlar"
                    },
                    "array": {
                        "contains": "İçerir",
                        "empty": "Boş",
                        "equals": "Eşittir",
                        "not": "Değildir",
                        "notEmpty": "Dolu",
                        "without": "Hariç"
                    }
                },
                "data": "Veri",
                "deleteTitle": "Filtreleme kuralını silin",
                "leftTitle": "Kriteri dışarı Urlıkart",
                "logicAnd": "ve",
                "logicOr": "veya",
                "rightTitle": "Kriteri içeri al",
                "title": {
                    "0": "Arama Oluşturucu",
                    "_": "Arama Oluşturucu (%d)"
                },
                "value": "Değer",
                "clearAll": "Filtreleri Temizle"
            },
            "searchPanes": {
                "clearMessage": "Hepsini Temizle",
                "collapse": {
                    "0": "Arama Bölmesi",
                    "_": "Arama Bölmesi (%d)"
                },
                "count": "{total}",
                "countFiltered": "{shown}\/{total}",
                "emptyPanes": "Arama Bölmesi yok",
                "loadMessage": "Arama Bölmeleri yükleniyor ...",
                "title": "Etkin filtreler - %d"
            },
            "searchPlaceholder": "Ara",
            "thousands": ".",
            "datetime": {
                "amPm": [
                    "öö",
                    "ös"
                ],
                "hours": "Saat",
                "minutes": "Dakika",
                "next": "Sonraki",
                "previous": "Önceki",
                "seconds": "Saniye",
                "unknown": "Bilinmeyen"
            },
            "decimal": ",",
            "editor": {
                "close": "Kapat",
                "create": {
                    "button": "Yeni",
                    "submit": "Kaydet",
                    "title": "Yeni kayıt oluştur"
                },
                "edit": {
                    "button": "Düzenle",
                    "submit": "Güncelle",
                    "title": "Kaydı düzenle"
                },
                "error": {
                    "system": "Bir sistem hatası oluştu (Ayrıntılı bilgi)"
                },
                "multi": {
                    "info": "Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak.",
                    "noMulti": "Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir.",
                    "restore": "Değişiklikleri geri al",
                    "title": "Çoklu değer"
                },
                "remove": {
                    "button": "Sil",
                    "confirm": {
                        "_": "%d adet kaydı silmek istediğinize emin misiniz?",
                        "1": "Bu kaydı silmek istediğinizden emin misiniz?"
                    },
                    "submit": "Sil",
                    "title": "Kayıtları sil"
                }
            }
        }
    });

    /* Datatable ends here. */

    $(function () {

        /* AJAX-GET  -> Getting and transporting _userAddPartial infos starts here. */

        const url = '/Admin/User/Add';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function (e) {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function (response) {
                console.error("Ajax Error:" + response);
            });
            e.preventDefault();
        });

        /* AJAX-GET  -> Getting and transporting _userAddPartial infos ends here. */
        /* AJAX-POST -> Posting form datas using userAddDto starts here. */

        placeHolderDiv.on('click', '#btnSave', function (e) {
            let form = $('#form-user-add');
            let actionUrl = form.attr('action');
            let dataToSend = new FormData(form.get(0)); // Get form data that it's index eq 0. 

            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    let userAddAjaxModel = jQuery.parseJSON(data);
                    let newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    let isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        dataTable.row.add([
                            userAddAjaxModel.UserDto.User.Id,
                            userAddAjaxModel.UserDto.User.UserName,
                            userAddAjaxModel.UserDto.User.Email,
                            userAddAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/img/${userAddAjaxModel.UserDto.User.Picture}" style="max-width:160px" alt="${userAddAjaxModel.UserDto.User.Picture}"/>`,
                            `<button class="btn btn-primary btn-edit btn-sm" data-id="${userAddAjaxModel.UserDto.User.Id}"><span class="fas fa-edit"></span></button>
                            <button class="btn btn-danger btn-delete btn-sm" data-id="${userAddAjaxModel.UserDto.User.Id}"><span class="fas fa-minus-circle"></span></button>`
                        ]).draw();

                        Swal.fire(
                            'Ekleme Başarılı!',
                            `${user.UserName} adlı kullanıcı başarıyla eklendi!`,
                            'success'
                        );
                    }
                    else {
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `*${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.error(err);
                }
            });
            e.preventDefault();
        });

        /* AJAX-POST -> Posting form datas using userAddDto ends here. */
        /* AJAX-POST -> Deleting users starts here. */

        $(document).on('click', '.btn-delete', function (e) {
            let id = $(this).data('id');
            let tableRow = $(`[name=row-${id}]`);
            let userName = tableRow.find('td:eq(1)').text(); 

            // Using 'Sweet Alert' library for asking "Are You Sure?" questions to user.
            Swal.fire({
                title: 'Silme İşlemi',
                text: `${userName} adlı kategoriyi silmek istediğinize emin misiniz?`,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet',
                cancelButtonText: 'Hayır'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            userId: id
                        },
                        url: '/Admin/User/Delete',
                        success: function (data) {
                            let userDto = jQuery.parseJSON(data);
                            if (userDto.ResultStatus === 0) {
                                Swal.fire(
                                    'Silme Başarılı!',
                                    `${userDto.User.UserName} adlı kullanıcı başarıyla silindi.`,
                                    'success'
                                );
                                dataTable.row.remove(tableRow).draw();
                            }
                            else {
                                Swal.fire(
                                    'Silme Başarısız!',
                                    `${userDto.Message}`,
                                    'error'
                                );
                            }
                        },
                        error: function (err) {
                            console.error(err);
                            toastr.error(`${err.responseText}`, "Hata !");
                        }
                    });
                }
            });
            e.preventDefault();
        });

        /* AJAX-POST -> Deleting users ends here. */

    });

    $(function () {

        // Autoloading process for UPDATING elements starts here.

        const url = '/Admin/User/Update';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-edit', function (e) {
            const id = $(this).attr('data-id');
            $.get(url, { userId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function (response) {
                console.error("Ajax Error:" + response);
            });
            e.preventDefault();
        });

        // Autoloading process for UPDATING elements ends here.
        // Updating users starts from here.

        placeHolderDiv.on('click', '#btnUpdateSave', function (e) {
            debugger
            let form = $('#form-user-update');
            let actionUrl = form.attr('action');
            let dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                let userUpdateAjaxModel = jQuery.parseJSON(data);
                console.log(userUpdateAjaxModel);
                let newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial); // Searching in object
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                let isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    let newTableRowObject = $(newTableRow);
                    let userTableRow = $(`[name="row-${userUpdateAjaxModel.UserDto.User.Id}"]`);
                    newTableRowObject.hide();
                    userTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(3000);
                    Swal.fire(
                        'Güncelleme Başarılı!',
                        `${userUpdateAjaxModel.UserDto.User.UserName} adlı kategori başarıyla güncellendi.`,
                        'success'
                    );
                } else {
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `*${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            }).fail(function (response) {
                console.error("Ajax Error:" + response);
            });

            e.preventDefault();
        });

        // Updating users ends from here.

    });



});