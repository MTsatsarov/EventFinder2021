import * as api from "./api.js"

export async function SendNewReply(data) {
    return await api.post(api.host + 'Reply/WriteReply', data);
}

export async function getAllComentaries(data) {
    return await api.get(api.host + 'Comentary/AllComentaries/'+data.eventId)
}

export async function getLikesDislikes(controller, action, data) {
    return await api.post(api.host + `${controller}/${action}`, data)
}

export async function sendNewComentary(data) {
    return await api.post(api.host + "Comentary/WriteComentary", data);

}

export async function getVotesGrade(data) {
    const result = await api.post(api.host + "api/Votes", data)
    return result.averageVoteValue.toFixed(1);
}
