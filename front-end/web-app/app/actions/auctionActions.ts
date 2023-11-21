'use server'

import {Auction, PagedResult} from "@/types";
import {getTokenWorkaround} from "@/app/actions/authActions";
import {fetchWrapper} from "@/lib/fetchWrapper";

export async function getData(urlQueryString: string): Promise<PagedResult<Auction>> {
    return await fetchWrapper.get(`search${urlQueryString}`)
}

export async function UpdateAuctionTest(){
    const data = {
        mileage: Math.floor(Math.random() * 100000 ) + 1
    }
    const token = await getTokenWorkaround();
    const res = await fetch('http://localhost:6001/auctions/d19fe601-0d63-4cc5-92ca-5904f6860744', {
        method: 'PUT',
        headers: {
            'Content-type': 'application/json',
            'Authorization': 'Bearer ' + token?.access_token
        },
        body: JSON.stringify(data)
    })
    
    if(!res.ok) {
        return {status: res.status,message: res.statusText}
    }
    
    return res.statusText
}