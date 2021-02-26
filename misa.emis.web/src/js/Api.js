const axios = require('axios')

var link = "http://localhost:49682/api/v1/"

// lấy danh sách entity
function getData(name) {
    return axios.get(link + name)
}

// lấy 1 entity theo id 
function getDataByID(name, id) {
    return axios.get(link + name + "/" + id)

}

// xóa 1 entity
async function deleteEntity(name, id) {
    var result = {}
    await axios.delete(
            link + name + "/" + id
        ).then((response) => {

            result = response
        })
        .catch((error) => {

            result = error.response
        })

    return result
}

//thêm mới 1 entity
async function insertEntity(name, entity) {
    var result = {}
    await axios
        .post(link + name + "/", entity)
        .then((response) => {

            result = response
        })
        .catch((error) => {

            result = error.response
        })

    return result
}


// chỉnh sửa 1 entity
async function updateEntity(name, entity, id) {
    var result = {}
    await axios
        .put("http://localhost:49682/api/v1/" + name + "/" + id, entity)
        .then((response) => {

            result = response
        })
        .catch((error) => {

            result = error.response
        })

    return result
}




export { getData, deleteEntity, insertEntity, updateEntity, getDataByID }