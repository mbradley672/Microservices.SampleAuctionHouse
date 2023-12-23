'use client'

import {Auction, PagedResult} from '@/types'
import React, {useEffect, useState} from "react"
import AuctionCard from "@/app/auctions/AuctionCard";
import AppPagination from "@/app/components/AppPagination";
import {getData} from "@/app/actions/auctionActions";
import Filters from "@/app/auctions/Filter";
import {useParamsStore} from "@/hooks/useParamsStore";
import {shallow} from "zustand/shallow";
import qs from 'query-string'
import EmptyFilter from "@/app/components/EmptyFilter";
import {useAuctionStore} from "@/hooks/useAuctioinStore";
import {RiLoaderFill} from "react-icons/ri";


export default function Listings() {
    const [loading, setLoading] = useState(true);
    const params = useParamsStore(state => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        searchTerm: state.searchTerm,
        orderBy: state.orderBy,
        filterBy: state.filterBy,
        seller: state.seller,
        winner: state.winner
    }), shallow)
    const data = useAuctionStore(state => ({
        auctions: state.auctions,
        totalCount: state.totalCount,
        pageCount: state.pageCount,
    }), shallow)
    const setData = useAuctionStore(state => state.setData)
    const setParams = useParamsStore(state => state.setParams)
    const url = qs.stringifyUrl({url: '', query: params})

    function setPageNumber(pageNumber: number) {
        setParams({pageNumber})
    }

    useEffect(() => {
        getData(url).then(data => {
            setData(data);
            setLoading(false)
        })
    }, [url])

    if (loading) return <><span className={'text-lg-center'}>Loading</span></>
    
    // @ts-ignore
    return (
        <>
            <Filters/>
            {
                data.totalCount === 0 ? (
                    <EmptyFilter showReset/>
                ) : (
                    <>
                        <div className={'grid grid-cols-4 gap-4'}>
                            {data.auctions.map((auction) => (
                                <AuctionCard auction={auction} key={auction.id}/>
                            ))}
                        </div>

                        <div className={'flex justify-center mt-4'}>
                            <AppPagination pageChanged={setPageNumber} currentPage={params.pageNumber}
                                           pageCount={data.pageCount}/>
                        </div>
                    </>
                )
            }
        </>
    )
}