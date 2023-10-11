'use client'

import React from "react";
import Image from "next/image";
import CountdownTimer from "@/app/auctions/CountdownTimer";
import CarImage from "@/app/auctions/CarImage";
import {Auction} from "@/types";

type Props = {
    auction: Auction,
    
}

export default function AuctionCard({auction}: Props) {
    return (
        <a href="#" className={'group border-amber-300 border-2 px-2 pt-2 pb-5 bg-blue-400 rounded-lg'}>
            <div className='w-full bg-gray-200 rounded-lg overflow-hidden aspect-h-10 aspect-w-16'>
                <div>
                    <CarImage imageUrl={auction.imageUrl} />
                    <div className={'absolute bottom-2 right-2'}>
                        <CountdownTimer auctionEnd={auction.auctionEnd} />
                    </div>
                </div>
                
            </div>
            <div className="flex justify-between items-center mt-4">
                <p className="text-grey-700 text-md-center">{auction.make} {auction.model}</p>
                <p className="font-semibold text-md-center">{auction.year}</p>
            </div>
        </a>
    )
}