$(document).ready(function() {
    loadPrivateTasks();

    $('button[data-bs-toggle="tab"]').on('shown.bs.tab', function(e) {
        const targetId = e.target.id;
        console.log(targetId)
        if (targetId === "my-tasks-tab") {
            loadPrivateTasks();
        } else {
            //loadGrups();
        }
    });

    $('#taskModal').on('show.bs.modal', function(e) {
        const select = $('#TaskGroupId');

        $.ajax({
            url: '/Group/GetMyGroups',
            method: 'GET',
            success: function(response) {
                if (response.success) {
                    select.empty();
                    select.append('<option value="" selected>Özel Görev (Sadece Ben)</option>');
                    
                    response.data.forEach(((taskGroup) => {
                        select.append(`<option value="${taskGroup.id}">${taskGroup.name}</option>`);
                    }))
                } else {
                    Swal.fire({
                        icon: "error",
                        title: "Hata!",
                        text: response.message,
                        confirmButtonText: "Tamam",
                    })
                }
            },
            error: function(xhr) {
                Swal.fire({
                    icon: "error",
                        title: "Hata!",
                        text: "Görev grupları getirilemedi. " + xhr.responseJSON.message,
                        confirmButtonText: "Tamam",
                })
            }
        })
    }) 

    $('#taskForm').on('submit', function(e) {
        e.preventDefault();

        const modal = $("#taskModal");
        const button = $('button[type="submit"]');
        button.attr('disabled', true).text("Kaydediliyor...");

        const formData = $(this).serialize();

        $.ajax({
            url: "/Task/Create",
            method: "POST",
            data: formData,
            success: function(response) {
                if (response.success) {
                    Swal.fire({
                        icon: "success",
                        title: "Başarılı!",
                        text: response.message,
                        confirmButtonText: "Tamam",
                        timer: 1500
                    });

                    loadPrivateTasks();
                } else {
                    Swal.fire({
                        title: 'Hata!',
                        text: response.message,
                        icon: 'error',
                        confirmButtonText: 'Tamam',
                    });
                }

                modal.modal('hide')
                $('#taskForm')[0].reset();
            },
            error: function(xhr) {
                Swal.fire({
                    title: 'Hata!',
                    text: `Hata Oluştu: ${xhr.responseJSON.message}`,
                    icon: 'error',
                    confirmButtonText: 'Tamam',
                });

                modal.modal('hide');
            },
            complete: function() {
                button.attr("disabled", false).text("Kaydet");
            }
        })
        
    });

})

function loadPrivateTasks() {
    const tbody = $('#myTasksContent tbody');

    $.ajax({
        url: '/Task/GetPrivateTasks',
        method: 'GET',
        success: function(response) {
            if (response.success) {
                tbody.empty();

                if (response.data.length === 0) {
                    tbody.append('<tr><td colspan="6" class="text-center text-muted">Henüz görev eklenmemiş.</td></tr>');
                    return;
                }

                response.data.forEach((task, index) => {
                    const priority = checkPriority(task.priority);

                    const row = `
                        <tr>
                            <td>${index + 1}</td>
                            <td>${task.title}</td>
                            <td><span class="badge ${priority.class}">${priority.text}</span></td>
                            <td>${new Date(task.createdAt).toLocaleDateString('tr-TR')}</td>
                            <td>${task.deadline && task.deadline !== 0
                                ? new Date(task.deadline).toLocaleDateString('tr-TR')
                                : 'Yok'}
                            </td>
                            <td class="text-end">
                                <button class="btn btn-sm btn-outline-info me-1" onclick="showTaskInfo(${task.id})">
                                    <i class="bi bi-eye"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-warning me-1" onclick="openEditModal(${task.id})">
                                    <i class="bi bi-pencil"></i>
                                </button>
                                <button class="btn btn-sm btn-outline-danger" onclick="deleteTask(${task.id})">
                                    <i class="bi bi-trash"></i>
                                </button>
                            </td>
                        </tr>`

                    tbody.append(row);
                })
            }
        },
        error: function() {
            tbody.html('<tr><td colspan="5" class="text-center text-danger">Veriler yüklenirken bir hata oluştu!</td></tr>');
        }
    })
}

function checkPriority(priority) {
    const map = {
        1: { text: 'Düşük', class: 'bg-success' },
        2: { text: 'Orta', class: 'bg-info' },
        3: { text: 'Yüksek', class: 'bg-warning text-dark' },
        4: { text: 'Acil', class: 'bg-danger' }
    };

    return map[priority]
}