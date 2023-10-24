'use server'

import {Auction, PagedResult} from "@/types";

export async function getData(urlQueryString: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${urlQueryString}`)
    if (!res.ok) throw new Error("Failed to fetch data")
    return res.json();
}

export async function UpdateAuctionTest(){
    const data = {
        mileage: Math.floor(Math.random() * 100000 ) + 1
    }
    
    const res = await fetch('http://localhost:6001/api/auctions', {
        method: 'PUT'
    })
}