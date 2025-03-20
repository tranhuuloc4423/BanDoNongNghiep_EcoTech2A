var laddaSubmitForm;
var dataTable;

$(document).ready(function () {

    //Init table
    let status = parseInt($('#search_status').val());
    LoadDataTable(0, 'GET', {
        status: status === -2 ? "0,1" : status,
    });

});

const buttonActionHtml = function (id, status, timer) {
    let html = ``;
    html += `<a href="javascript:void(0);" class="action-icon" onclick="ShowEditModal(this,${id})" title="${_buttonResource.Edit}">${_iconEditHtml}</a>`;
    html += `<a href="javascript:void(0);" class="action-icon" onclick="ShowDeleteModal(this,${id})" title="${_buttonResource.Delete}">${_iconDeleteHtml}</a> `;
    if (parseInt(status) !== -1)
        html += `<div class="custom-control custom-switch" style="position: initial; display: inline;">
                  <input type="checkbox" class="custom-control-input custom-control-input-success" id="status_${id}" ${parseInt(status) == 0 ? "" : "checked"} onclick="ChangeStatus(this, event, '${id}', '${timer}')">
                  <label class="custom-control-label" style="margin-top:4px;" for="status_${id}"></label>
                </div>`;
    return html;
}

//Load table
function LoadDataTable(isRefresh, method = 'GET', params) {
    if (isRefresh == 1) { //isRefresh (0: init table | 1: refresh table)
        dataTable.destroy();
    }
    dataTable = $('#table_main').DataTable({
        lengthChange: true,
        lengthMenu: [[10, 25, 50, 100, -1], ['10', '25', '50', '100', 'Tất cả']],
        colReorder: {
            allowReorder: false
        },
        responsive: { details: true },
        scrollX: true,
        select: false,
        stateSave: false,
        ajax: {
            type: method,
            url: '/StudentManage/GetList',
            data: params,
            dataType: 'json',
            dataSrc: function (response) {
                if (CheckResponseIsSuccess(response))
                    return response.data;
                return [];
            },
            error: function (err) {
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        },
        columns: [
            {
                data: null,
                render: (data, type, row, meta) => ++meta.row,
                width: "25px"
            },
            {
                data: "id", render: function (data, type, row, meta) {
                    return buttonActionHtml(data, row.status, row.timer);
                },
                orderable: false,
                searchable: false,
                className: "text-center"
            },
            {
                data: "firstName",
                render: function (data, type, row, meta) {
                    return `<a href="javascript:void(0)" onclick="ShowViewModal(this,${row.id})"><u>${IsNullOrEmty(data) ? + "" : data}</u></a>`;
                }
            },
            {
                data: "lastName",
                render: function (data, type, row, meta) {
                    return `<a href="javascript:void(0)" onclick="ShowViewModal(this,${row.id})"><u>${IsNullOrEmty(data) ? + "" : data}</u></a>`;
                }
            },
            {
                data: "email"
            },
            {
                data: "phoneNumber"
            },
            {
                data: "gender",
                render: function (data, type, row, meta) {
                    return data === 1 ? 'Nữ' : 'Nam';
                },
                className: "text-center"
            },
            {
                data: "birthday",
                render: function (data, type, row, meta) {
                    return !IsNullOrEmty(data) ? moment(data).format('YYYY-MM-DD') : '';
                },
                className: "text-center"
            },
            {
                data: "createdAt",
                render: function (data, type, row, meta) {
                    return !IsNullOrEmty(data) ? moment(data).format('YYYY-MM-DD') : '';
                },
                className: "text-center"
            }
        ],
        language: _languageDataTalbeObj,
        drawCallback: _dataTablePaginationStyle,
        //initComplete: function () {}
    });

}

//Search 
function Search() {
    let status = parseInt($('#search_status').val());
    LoadDataTable(1, 'GET', {
        status: status === -2 ? "0,1" : status,
    });
}

//Show panel when done
function ShowPanelWhenDone(html) {
    $(window).scrollTop();
    $('#div_view_panel').html(html);
    ShowHidePanel("#div_view_panel", "#div_main_table");
}

//Show add modal
function ShowAddModal(elm) {
    let laddaShow = Ladda.create(elm);
    laddaShow.start();
    $.get('/StudentManage/P_Add').done(function (response) {
        laddaShow.stop();
        if (response.result === -1 || response.result === 0) {
            CheckResponseIsSuccess(response); return false;
        }
        ShowPanelWhenDone(response);
        $.Components.init(); //Init form validation
        InitSubmitAddForm();
    }).fail(function (err) {
        laddaShow.stop();
        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
    });
}

//Show edit modal
function ShowEditModal(elm, id) {
    let text = $(elm).html();
    $(elm).attr('disabled', true); $(elm).html(_loadAnimationSmallHtml); ShowOverlay('#div_main_table');
    $.get(`/StudentManage/P_Edit/${id}`).done(function (response) {
        HideOverlay('#div_main_table'); $(elm).attr('disabled', false); $(elm).html(text);
        if (response.result === -1 || response.result === 0) {
            CheckResponseIsSuccess(response); return false;
        }
        ShowPanelWhenDone(response);
        $.Components.init(); //Init form validation
        InitSubmitEditForm();
    }).fail(function (err) {
        HideOverlay('#div_main_table'); $(elm).attr('disabled', false); $(elm).html(text);
        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
    });
}

//Show view modal
function ShowViewModal(elm, id) {
    let text = $(elm).html();
    $(elm).attr('disabled', true); $(elm).html(_loadAnimationSmallHtml); ShowOverlay('#div_main_table');
    $.get(`/StudentManage/P_View/${id}`).done(function (response) {
        HideOverlay('#div_main_table'); $(elm).attr('disabled', false); $(elm).html(text);
        if (response.result === -1 || response.result === 0) {
            CheckResponseIsSuccess(response); return false;
        }
        ShowPanelWhenDone(response);
    }).fail(function (err) {
        HideOverlay('#div_main_table'); $(elm).attr('disabled', false); $(elm).html(text);
        CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
    });
}

//Show delete modal
function ShowDeleteModal(elm, id) {
    $(elm).attr('disabled', true);
    $("#delete_id").val(id);
    $('#delete_modal').modal('show');
    setTimeout(() => $(elm).attr('disabled', false), 500);
}

//Delete
function Delete() {
    let id = $("#delete_id").val();
    $.ajax({
        type: 'POST',
        url: '/StudentManage/Delete',
        data: { id: id },
        dataType: 'json',
        success: function (response) {
            $('#delete_modal').modal('hide');
            if (!CheckResponseIsSuccess(response)) return false;
            ShowToastNoti('success', '', _resultActionResource.DeleteSuccess);
            ChangeUIDelete(dataTable, id);
        },
        error: function (err) {
            CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
        }
    });
}

//Init submit add form
function InitSubmitAddForm() {
    $('#form_data_add').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        let $formElm = $('#form_data_add');
        //let isvalidate = $formElm[0].checkValidity();
        let isvalidate = CheckValidationUnobtrusive($formElm);
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_form_add'));
        laddaSubmitForm.start();
        $.ajax({
            url: '/StudentManage/P_Add',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) return false;
                ShowToastNoti('success', '', _resultActionResource.AddSuccess);
                BackToTable();
                if (CheckNewRecordIsAcceptAddTable(response.data)) ChangeUIAdd(dataTable, response.data);
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    });
}

//Init submit edit form
function InitSubmitEditForm() {
    $('#form_data_edit').on('submit', function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();
        let $formElm = $('#form_data_edit');
        //let isvalidate = $formElm[0].checkValidity();
        let isvalidate = CheckValidationUnobtrusive($formElm);
        if (!isvalidate) { ShowToastNoti('warning', '', _resultActionResource.PleaseWrite); return false; }
        let formData = new FormData($formElm[0]);
        laddaSubmitForm = Ladda.create(document.querySelector('#btn_submit_form_edit'));
        laddaSubmitForm.start();
        $.ajax({
            url: '/StudentManage/P_Edit',
            type: 'POST',
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                laddaSubmitForm.stop();
                if (!CheckResponseIsSuccess(response)) return false;
                ShowToastNoti('success', '', _resultActionResource.UpdateSuccess);
                BackToTable(); ChangeUIEdit(dataTable, response.data.id, response.data);
            }, error: function (err) {
                laddaSubmitForm.stop();
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    });
}

//Change status
function ChangeStatus(elm, e, id, timer) {
    if ($(elm).data('clicked')) {
        e.preventDefault();
        e.stopPropagation();
    } else {
        $(elm).data('clicked', true);//Mark to ignore next click
        window.setTimeout(() => $(elm).removeData('clicked'), 800);//Unmark after time
        $(elm).attr('onclick', "event.preventDefault();");
        $('#status_' + id).parent().find('label.btn-active').attr('onclick', 'event.preventDefault()');
        var isChecked = $('#status_' + id).is(":checked");
        $.ajax({
            type: 'POST',
            url: '/StudentManage/ChangeStatus',
            data: {
                id: id,
                status: isChecked ? 1 : 0,
                timer: timer
            },
            dataType: 'json',
            success: function (response) {
                if (!CheckResponseIsSuccess(response)) {
                    $(elm).attr('onclick', `ChangeStatus(this, event, ${id})`); return false;
                }
                ShowToastNoti('success', '', _resultActionResource.UpdateSuccess);
                window.setTimeout(function () {
                    $(elm).attr('onclick', `ChangeStatus(this, event, ${response.data.id}, '${response.data.timer}')`);
                    ChangeUIEdit(dataTable, response.data.id, response.data);
                }, 500);
            }, error: function (err) {
                $(elm).attr('onclick', `ChangeStatus(this, event, ${id})`);
                CheckResponseIsSuccess({ result: -1, error: { code: err.status } });
            }
        });
    }
}

//Check new record isvalid
function CheckNewRecordIsAcceptAddTable(data) {
    let condition = true; //place condition expression in here
    return condition;
}
