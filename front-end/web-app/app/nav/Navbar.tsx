import React from "react";
import {AiOutlineCar} from "react-icons/ai";
import {LuSearch} from "react-icons/lu";
import Search from "@/app/nav/Search";
import dynamic from "next/dynamic";

export default function Navbar() {
    // Need to change the Search to NoSSR so that we can deal with any browserplugins like NordPass
    const NoSSRSearchComponent = dynamic(async () => Search, {ssr: false})
    return (
        <header
            className={'sticky top-0 z-50 flex justify-between p-5 items-center text-gray-800 shadow-md rounded-b-2xl bg-blue-400'}>
            <div className={'flex items-center gap-2 text-3xl font-semibold text-amber-300'}>
                <AiOutlineCar size={34}/>
                Car Auctions
            </div>
            <Search />
            <div>
                Login
            </div>
        </header>
    )
}