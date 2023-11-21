import {getToken} from "next-auth/jwt";
import {getTokenWorkaround} from "@/app/actions/authActions";
import {Headers} from "next/dist/compiled/@edge-runtime/primitives";

const BASE_URL = 'http://localhost:6001/'

async function get(url: string) {
    const requestOptions = {
        method: 'GET',
        headers: await getHeaders()
    }
    const response = await fetch(BASE_URL + url, requestOptions);
    return await handleResponse(response);
}
async function post(url: string, body: {}) {
    const requestOptions = {
        method: 'POST',
        body: JSON.stringify(body),
        headers: await getHeaders()
    }
    const response = await fetch(BASE_URL + url, requestOptions);
    return await handleResponse(response);
}

async function put(url: string, body: {}) {
    const requestOptions = {
        method: 'PUT',
        body: JSON.stringify(body),
        headers: await getHeaders()
    }
    const response = await fetch(BASE_URL + url, requestOptions);
    return await handleResponse(response);
}

async function del(url: string, body: {}) {
    const requestOptions = {
        method: 'DELETE',
        headers: await getHeaders()
    }
    const response = await fetch(BASE_URL + url, requestOptions);
    return await handleResponse(response);
}

async function getHeaders() {
    const token = await getTokenWorkaround();
    const headers = {'Content-type': 'application/json'} as any;
    if(token) {
        headers.Authorization = `Bearer ${token.access_token}`
    }
    return headers;
}
async function handleResponse(response: Response) {
    const text = await response.text();
    const data = text && JSON.parse(text);
    
    if (response.ok){
        return data || response.statusText;
    }
    else {
        return {
            status: response.status,
            message: response.statusText
        };
    }
}

export const fetchWrapper = {
    get, post, put, del
}