import React from "react";
import Image from "next/image";

type Props = {
    auction: any,
    
}

export default function AuctionCard({auction}: Props) {
    return (
        <a href="#">
            <div className='w-full bg-gray-200 rounded-lg overflow-hidden aspect-h-10 aspect-w-16'>
                <Image src={auction.imageUrl} alt={'image'} fill className={'object-cover'} />
            </div>
            
        </a>
    )
}