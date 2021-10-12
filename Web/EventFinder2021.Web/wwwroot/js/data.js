import * as api from "./api.js"

export async function SendNewReply(data) {
    return await api.post(api.host + 'Reply/WriteReply', data);
}

export async function getAllComentaries(data) {
    return await api.post(api.host + 'Comentary/AllComentaries', data)
}

export async function getLikesDislikes(controller, action, data) {
    return await api.post(api.host + `${controller}/${action}`, data)
}

export async function sendNewComentary(data) {
    return await api.post(api.host + "Comentary/WriteComentary", data);

}