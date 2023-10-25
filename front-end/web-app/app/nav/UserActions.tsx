'use client'

import React from 'react'
import {Button, Dropdown} from "flowbite-react";
import Link from "next/link";
import {User} from "next-auth";
import {HiCog, HiUser} from "react-icons/hi2";
import {AiFillCar, AiFillTrophy, AiOutlineLogout} from "react-icons/ai";
import {DropdownDivider} from "flowbite-react/lib/esm/components/Dropdown/DropdownDivider";
import {signOut} from "next-auth/react";
import {defaultFlowbiteTheme} from "@/themes/DefaultFlowbiteTheme";
import {usePathname, useRouter} from "next/navigation";
import {useParamsStore} from "@/hooks/useParamsStore";

type Props = {
    user: User
}
export default function UserActions({user}: Props) {
    const router = useRouter();
    const pathname = usePathname();
    const setParams = useParamsStore(state => state.setParams)
    
    function setWinner(){
        setParams({winner: user.username, seller: undefined})
        if (pathname !== '/') router.push('/');
    }

    function setSeller(){
        setParams({winner: undefined, seller: user.username})
        if (pathname !== '/') router.push('/');
    }
    
    if (!user) {
        return <div>Loading...</div>
    }

    return (
        <Dropdown color={'blue'} label={`Welcome ${user.name}`}>
            <Dropdown.Item icon={HiUser} onClick={setSeller}>
                    My Auctions
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillTrophy} onClick={setWinner}>
                    Auctions Won
            </Dropdown.Item>
            <Dropdown.Item icon={AiFillCar}>
                <Link href={'/auctions/create'}>
                    Sell my car
                </Link>
            </Dropdown.Item>
            <Dropdown.Item icon={HiCog}>
                <Link href={'/session'}>
                    Session (dev only)
                </Link>
            </Dropdown.Item>
            <DropdownDivider />
            <Dropdown.Item icon={AiOutlineLogout} onClick={() => signOut({callbackUrl: '/'})}>
                Logout
            </Dropdown.Item>
        </Dropdown>
    )
}