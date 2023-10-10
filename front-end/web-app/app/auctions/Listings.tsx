'use client'

import {Auction} from '@/types'
import React, {useEffect, useState} from "react"
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/components/AppPagination";
import {getData} from "@/app/actions/auctionActions";
import Filters from "@/app/auctions/Filter";

export default function Listings() {
    const [auctions,setAuctions] = useState<Auction[]>([])
    const [pageCount,setPageCount] = useState<number>(0)
    const [pageNumber,setPageNumber] = useState<number>(1)
    const [pageSize, setPageSize] = useState<number>(4)
    useEffect(()=>{
        getData(pageNumber, pageSize).then(data =>{
            setAuctions(data.results)
            setPageCount(data.pageCount)
        })
    },[pageNumber, pageSize])
    
    if (auctions.length === 0) return (<><h3 suppressHydrationWarning={true}>Loading...</h3></>)
    else
        return (
            <>
                <Filters pageSize={4} setPageSize={setPageSize}/>
                <div className={'grid grid-cols-4 gap-4'}>
                    {auctions && auctions.map((auction) => (
                        <AuctionCard auction={auction} key={auction.id}/>
                    ))}
                </div>
                <div className={'flex justify-center mt-4'}>
                    <AppPagination pageChanged={setPageNumber} currentPage={pageNumber} pageCount={pageCount}/>
                </div>
            </>
        )
}