'use server'

import {Auction, PagedResult} from "@/types";

/**
 * Retrieves data from the specified API endpoint.
 *
 * @async
 * @function getData
 * @throws {Error} Failed to fetch data.
 * @returns {Promise<Object>} A promise that resolves to the retrieved data as an object.
 */
export async function getData(urlQueryString: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${urlQueryString}`)
    if (!res.ok) throw new Error("Failed to fetch data")
    return res.json();
}