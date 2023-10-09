import React from "react";
import {AiOutlineCar} from "react-icons/ai";
import {LuSearch} from "react-icons/lu";

export default function Navbar() {
    return (
        <header
            className={'sticky top-0 z-50 flex justify-between bg-white p-5 items-center text-gray-800 shadow-md rounded-b-2xl'}>
            <div className={'flex items-center gap-2 text-3xl font-semibold text-red-500'}>
                <AiOutlineCar size={34}/>
                Car Auctions
            </div>
            <div className={'flex items-center gap-2 text-2xl font-semibold text-gray-600'}>
                <LuSearch/>
                Search
            </div>
            <div>
                Login
            </div>
        </header>
    )
}