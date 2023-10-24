import React from "react";
import Search from "@/app/nav/Search";
import LoginButton from "@/app/nav/LoginButton";
import Logo from "@/app/nav/Logo";
import {getCurrentUser} from "@/app/actions/authActions";
import UserActions from "@/app/nav/UserActions";

export default async function Navbar() {
    const user = await getCurrentUser();
    
    return (
        <header
            className={'sticky top-0 z-50 flex justify-between p-5 items-center text-gray-800 shadow-md rounded-b-2xl bg-blue-400'}>
            <Logo />
            <Search />
            {user ? (
                <UserActions user={user}/>
            ): (
                <LoginButton />
            )}
        </header>
    )
}