import React from "react"
import AuctionCard from "@/app/auctions/AuctionCard";

/**
 * Retrieves data from the specified API endpoint.
 *
 * @async
 * @function getData
 * @throws {Error} Failed to fetch data.
 * @returns {Promise<Object>} A promise that resolves to the retrieved data as an object.
 */
async function getData() {
    const res = await fetch('http://localhost:6001/search?pageSize=10')
    if (!res.ok) throw new Error("Failed to fetch data")
    return res.json();
}

export default async function Listings() {
    const data = await getData()
    return (

            
        <div className={'grid grid-cols-4 gap-4'}>
            {data && data.results.map((auction: any) => (
                <AuctionCard auction={auction} key={auction.id}/>
            ))}
        </div>
    )
}