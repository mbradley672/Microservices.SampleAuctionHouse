'use client'

import {Auction, PagedResult} from '@/types'
import React, {useEffect, useState} from "react"
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/components/AppPagination";
import {getData} from "@/app/actions/auctionActions";
import Filters from "@/app/auctions/Filter";
import {useParamsStore} from "@/hooks/useParamsStore";
import {stat} from "fs";
import {shallow} from "zustand/shallow";
import qs from 'query-string'


export default function Listings() {
    const [data, setData] = useState<PagedResult<Auction>>();
    const params = useParamsStore(state => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        searchTerm: state.searchTerm
    }), shallow)
    const setParams = useParamsStore(state => state.setParams)
    const url = qs.stringifyUrl({url: '', query: params})

    function setPageNumber(pageNumber: number){
        setParams({pageNumber})
    }
    
    useEffect(()=>{
        getData(url).then(data =>{
            setData(data)
        })
    },[url])
    
    if (!data) return <><span className={'text-lg-center'}>Loading</span></>
    else
        { // @ts-ignore
            return (
                        <>
                            <Filters />
                            // @ts-ignore
                            <div className={'grid grid-cols-4 gap-4'}>
                                {data.results.map((auction) => (
                                    <AuctionCard auction={auction} key={auction.id}/>
                                ))}
                            </div>
                            // @ts-ignore
                            <div className={'flex justify-center mt-4'}>
                                <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber} pageCount={data.pageCount}/>
                            </div>
                        </>
                    )
        }
}