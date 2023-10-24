'use server'

import {Auction, PagedResult} from "@/types";
import {getTokenWorkaround} from "@/app/actions/authActions";

export async function getData(urlQueryString: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${urlQueryString}`)
    if (!res.ok) throw new Error("Failed to fetch data")
    return res.json();
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