import React from 'react'
import {Prata} from "next/dist/compiled/@next/font/dist/google";

export default function AuctionDetails({params}: {params: {id: string}}) {


    return (
        <>
            AuctionDetails for {params.id}
        </>
    )
}